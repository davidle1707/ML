using log4net;
using ML.Common;
using ML.Utils.Phone.Vendors.Plivo;
using ML.Utils.Phone.Vendors.Plivo.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ML.Utils.Phone
{
    public static partial class PhoneExtensions
    {
        #region Phone

        public static GetPhoneNumberResponse GetPhoneNumbers(this PlivoManager manager, GetPhoneNumberRequest request)
        {
            return manager.GetPhoneNumbersAsync(request).Result;
        }

        public static async Task<GetPhoneNumberResponse> GetPhoneNumbersAsync(this PlivoManager manager, GetPhoneNumberRequest request)
        {
            var response = new GetPhoneNumberResponse { Success = true };

            var searchResponse = await manager.SearchPhoneNumbersAsync(new SearchPhoneNumberRequest
            {
                country_iso = request.CountryCode,
                region = !string.IsNullOrEmpty(request.StateCode) ? request.StateCode : null,
                type = request.IsTollFree ? "tollfree" : "fixed",
                services = "sms",
                pattern = !string.IsNullOrEmpty(request.PhoneNumber) ? request.PhoneNumber : null,
                offset = (Math.Max(request.Page, 1) - 1) * 20
            });

            if (searchResponse.Data?.error != null || searchResponse.Data?.objects == null)
            {
                response.Success = false;

                if (searchResponse.Data?.error != null)
                {
                    response.Description = searchResponse.Data.error;
                }

                return response;
            }

            response.TotalPages = (int)(Math.Ceiling(searchResponse.Data.meta.total_count / (double)20));

            response.PhoneNumbers = searchResponse.Data.objects.Select(s => new AvailablePhoneNumber
            {
                VendorType = VendorType.Plivo,
                PhoneNumber = s.number,
                FriendlyName = s.number.FormatPhoneNumber(),
                IsoCountry = s.country,
                Lata = s.lata.ToStr(),
                RateCenter = s.rate_center,
                Region = s.region
            }).ToList();

            return response;
        }

        public static OrderPhoneResponse OrderPhoneNumber(this PlivoManager manager, string phoneNumber, string appId = "")
        {
            return manager.OrderPhoneNumberAsync(phoneNumber, appId).Result;
        }

        public static async Task<OrderPhoneResponse> OrderPhoneNumberAsync(this PlivoManager manager, string phoneNumber, string appId = "")
        {
            var response = new OrderPhoneResponse();

            var orderRequest = new BuyPhoneNumberRequest { number = phoneNumber };

            if (!string.IsNullOrWhiteSpace(appId))
            {
                orderRequest.app_id = appId;
            }

            var orderResponse = await manager.BuyPhoneNumberAsync(orderRequest);

            if (orderResponse == null || orderResponse.Data == null)
            {
                response.Description = "Cannot order this phone";

                return response;
            }

            if (!string.IsNullOrWhiteSpace(orderResponse.Data.error) || orderResponse.Data.numbers == null || orderResponse.Data.numbers.Count == 0)
            {
                response.Description = orderResponse.Data.error;

                return response;
            }

            response.Success = true;
            response.ExternalId = orderResponse.Data.numbers[0].number;

            return response;
        }

        public static DeletePhoneResponse DeleteOrderPhoneNumber(this PlivoManager manager, string phoneNumber)
        {
            return manager.DeleteOrderPhoneNumberAsync(phoneNumber).Result;
        }

        public static async Task<DeletePhoneResponse> DeleteOrderPhoneNumberAsync(this PlivoManager manager, string phoneNumber)
        {
            var deleteResponse = await manager.UnrentNumberAsync(new BuyPhoneNumberRequest { number = phoneNumber.ToPhoneNumber() });

            var response = new DeletePhoneResponse
            {
                Success = deleteResponse.Data != null && string.IsNullOrWhiteSpace(deleteResponse.Data.error)
            };

            if (!response.Success)
            {
                response.Description = deleteResponse.Data != null ? deleteResponse.Data.error : deleteResponse.ErrorMessage;
            }

            return response;
        }

        #endregion

        #region Message

        public static SendSmsResponse SendSms(this PlivoManager manager, string to, string message, ILog log = null)
        {
            return manager.SendSmsAsync(manager.Setting.From, to, message, log).Result;
        }

        public static Task<SendSmsResponse> SendSmsAsync(this PlivoManager manager, string to, string message, ILog log = null)
        {
            return manager.SendSmsAsync(manager.Setting.From, to, message, log);
        }

        public static SendSmsResponse SendSms(this PlivoManager manager, string from, string to, string message, ILog log = null)
        {
            return manager.SendSmsAsync(from, to, message, log).Result;
        }

        public static async Task<SendSmsResponse> SendSmsAsync(this PlivoManager manager, string from, string to, string message, ILog log = null)
        {
            var response = new SendSmsResponse { Status = SendSmsStatus.Unsuccess };

            try
            {
                //note: plivo use full phone number (11 characters)
                var result = await manager.SendMessageAsync(new SendMessageRequest { src = Util.ToPhoneNumber(from), dst = Util.ToPhoneNumber(to), text = message });

                if (result == null || result.Data == null)
                {
                    return response;
                }

                if (!string.IsNullOrWhiteSpace(result.Data.error) || result.Data.message_uuid == null)
                {
                    response.Description = result.Data.error;
                    return response;
                }

                response.Status = SendSmsStatus.Success;
                response.ExternalSmsJson = JsonConvert.SerializeObject(result.Data);

                if (result.Data.message_uuid.Count > 0)
                {
                    response.ExternalSmsId = result.Data.message_uuid[0];
                }
            }
            catch (Exception ex)
            {
                response.Status = SendSmsStatus.Error;
                response.Description = ex.Message;

                if (log != null)
                {
                    log.Error("Error when sending sms by Plivo", ex);
                }
            }

            return response;
        }

        #endregion

    }
}
