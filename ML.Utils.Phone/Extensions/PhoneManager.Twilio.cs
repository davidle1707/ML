using log4net;
using ML.Common;
using ML.Utils.Phone.Vendors.Twilio;
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

        public static GetPhoneNumberResponse GetPhoneNumbers(this TwilioManager manager, GetPhoneNumberRequest request)
        {
            return manager.GetPhoneNumbersAsync(request).Result;
        }

        public static async Task<GetPhoneNumberResponse> GetPhoneNumbersAsync(this TwilioManager manager, GetPhoneNumberRequest request)
        {
            var response = new GetPhoneNumberResponse { Success = true };

            var options = new AvailablePhoneNumberListRequest
            {
                Contains = string.IsNullOrEmpty(request.PhoneNumber) ? string.Empty : request.PhoneNumber,
                InRegion = request.StateCode
            };

            var searchResponse = request.IsTollFree
                ? await manager.ListAvailableTollFreePhoneNumbersAsync(request.CountryCode, options)
                : await manager.ListAvailableLocalPhoneNumbersAsync(request.CountryCode, options);

            if (searchResponse.RestException != null || searchResponse.AvailablePhoneNumbers == null)
            {
                response.Success = false;

                if (searchResponse.RestException != null)
                {
                    response.Description = searchResponse.RestException.Message;
                }

                return response;
            }

            //twilio not support paging, the list of phone nuymbers will be refresh automatically for each call get list
            response.TotalPages = 50;

            response.PhoneNumbers = searchResponse.AvailablePhoneNumbers.Select(s => new AvailablePhoneNumber
            {
                VendorType = VendorType.Twilio,
                PhoneNumber = s.PhoneNumber,
                FriendlyName = s.FriendlyName,
                IsoCountry = s.IsoCountry,
                Lata = s.Lata,
                Latitude = s.Latitude,
                Longitude = s.Longitude,
                PostalCode = s.PostalCode,
                RateCenter = s.RateCenter,
                Region = s.Region
            }).ToList();

            return response;
        }

        public static OrderPhoneResponse OrderPhoneNumber(this TwilioManager manager, string phoneNumber, string appId = "")
        {
            return manager.OrderPhoneNumberAsync(phoneNumber, appId).Result;
        }

        public static async Task<OrderPhoneResponse> OrderPhoneNumberAsync(this TwilioManager manager, string phoneNumber, string appId = "")
        {
            var response = new OrderPhoneResponse();

            var request = new PhoneNumberOptions { PhoneNumber = phoneNumber };

            if (!string.IsNullOrWhiteSpace(appId))
            {
                request.SmsApplicationSid = appId;
            }

            var orderResponse = await manager.AddIncomingPhoneNumberAsync(request);

            if (string.IsNullOrWhiteSpace(orderResponse?.Sid))
            {
                return response;
            }

            response.Success = true;
            response.ExternalId = orderResponse.Sid;

            return response;
        }

        public static DeletePhoneResponse DeleteOrderPhoneNumber(this TwilioManager manager, string id)
        {
            return DeleteOrderPhoneNumberAsync(manager, id).Result;
        }

        public static async Task<DeletePhoneResponse> DeleteOrderPhoneNumberAsync(this TwilioManager manager, string id)
        {
            var deleteResponse = await manager.DeleteIncomingPhoneNumberAsync(id);

            var response = new DeletePhoneResponse { Success = deleteResponse == DeleteStatus.Success };

            return response;
        }

        #endregion

        #region Message

        public static SendSmsResponse SendSms(this TwilioManager manager, string to, string message, ILog log = null)
        {
            return manager.SendSmsAsync(manager.Setting.From, to, message, log).Result;
        }

        public static SendSmsResponse SendSms(this TwilioManager manager, string from, string to, string message, ILog log = null)
        {
            return manager.SendSmsAsync(from, to, message, log).Result;
        }

        public static Task<SendSmsResponse> SendSmsAsync(this TwilioManager manager, string to, string message, ILog log = null)
        {
            return manager.SendSmsAsync(manager.Setting.From, to, message, log);
        }

        public static async Task<SendSmsResponse> SendSmsAsync(this TwilioManager manager, string from, string to, string message, ILog log = null)
        {
            var response = new SendSmsResponse { Status = SendSmsStatus.Unsuccess };

            try
            {
                var result = await manager.SendSmsMessageAsync(from, to, message);

                if (result != null)
                {
                    response.ExternalSmsId = result.Sid;
                    response.ExternalSmsPrice = result.Price;
                    response.ExternalSmsJson = JsonConvert.SerializeObject(result, Formatting.None);

                    if (!string.IsNullOrEmpty(result.Sid) && ";QUEUED;SENDING;SENT;".Contains(";" + result.Status.ToStr().ToUpper() + ";"))
                    {
                        response.Status = SendSmsStatus.Success;
                        return response;
                    }

                    if (result.RestException != null)
                    {
                        var excep = result.RestException;
                        if (";400;401;404;405;500;".Contains(";" + excep.Status.ToStr().ToUpper() + ";"))
                        {
                            response.Status = SendSmsStatus.Unsuccess;
                            response.Description = excep.Message;
                            return response;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.Status = SendSmsStatus.Error;
                response.Description = ex.Message;

                if (log != null)
                {
                    log.Error("Error when sending sms by Twilio", ex);
                }
            }

            return response;
        }

        public static List<Message> GetListMessage(this TwilioManager manager, string from, string to, DateTime? dateSent)
        {
            return manager.GetListMessage(from, to, dateSent);
        }

        #endregion
    }
}
