using System.Threading.Tasks;

namespace ML.Utils.Phone
{
    public partial class PhoneManager
    {
        public GetPhoneNumberResponse GetAllPhoneNumbers(GetPhoneNumberRequest request)
        {
            return GetAllPhoneNumbersAsync(request).Result;
        }

        public async Task<GetPhoneNumberResponse> GetAllPhoneNumbersAsync(GetPhoneNumberRequest request)
        {
            var response = new GetPhoneNumberResponse { Success = true };

            GetPhoneNumberResponse plivoResponse = null, twilioResponse = null;

            if (Plivo.Setting != null)
            {
                plivoResponse = await Plivo.GetPhoneNumbersAsync(request);
                response.PhoneNumbers.AddRange(plivoResponse.PhoneNumbers);
            }

            if (Twilio.Setting != null)
            {
                twilioResponse = await Twilio.GetPhoneNumbersAsync(request);
                response.PhoneNumbers.AddRange(twilioResponse.PhoneNumbers);
            }

            if (!(plivoResponse != null && plivoResponse.Success) && !(twilioResponse != null && twilioResponse.Success))
            {
                response.Success = false;
                response.Description = "All vendors are invalid.";
            }

            return response;
        }

        public GetPhoneNumberResponse GetPhoneNumbers(VendorType vendor, GetPhoneNumberRequest request)
        {
            return GetPhoneNumbersAsync(vendor, request).Result;
        }

        public async Task<GetPhoneNumberResponse> GetPhoneNumbersAsync(VendorType vendor, GetPhoneNumberRequest request)
        {
            switch (vendor)
            {
                case VendorType.Plivo:
                    return await Plivo.GetPhoneNumbersAsync(request);

                case VendorType.Twilio:
                    return await Twilio.GetPhoneNumbersAsync(request);

                default:
                    return new GetPhoneNumberResponse { Success = false, Description =
                                                                             $"{vendor} not support this feature."
                                                      };
            }
        }

        /// <summary>
        /// Use ApplicationId in vendor setting (Plivo, Twilio)
        /// </summary>
        public OrderPhoneResponse OrderPhoneNumber(VendorType vendor, string phoneNumber)
        {
            return OrderPhoneNumberAsync(vendor, phoneNumber).Result;
        }

        /// <summary>
        /// Use ApplicationId in vendor setting (Plivo, Twilio)
        /// </summary>
        public async Task<OrderPhoneResponse> OrderPhoneNumberAsync(VendorType vendor, string phoneNumber)
        {
            switch (vendor)
            {
                case VendorType.Plivo:
                    return await Plivo.OrderPhoneNumberAsync(phoneNumber, Plivo.Setting.ApplicationId);

                case VendorType.Twilio:
                    return await Twilio.OrderPhoneNumberAsync(phoneNumber, Twilio.Setting.ApplicationId);

                default:
                    return new OrderPhoneResponse { Description = "Order feature is not support for " + vendor };
            }
        }

        /// <summary>
        /// appId is empty -> order phone not use application
        /// </summary>
        public OrderPhoneResponse OrderPhoneNumber(VendorType vendor, string phoneNumber, string appId)
        {
            return OrderPhoneNumberAsync(vendor, phoneNumber, appId).Result;
        }

        /// <summary>
        /// appId is empty -> order phone not use application
        /// </summary>
        public async Task<OrderPhoneResponse> OrderPhoneNumberAsync(VendorType vendor, string phoneNumber, string appId)
        {
            switch (vendor)
            {
                case VendorType.Plivo:
                    return await Plivo.OrderPhoneNumberAsync(phoneNumber, appId);

                case VendorType.Twilio:
                    return await Twilio.OrderPhoneNumberAsync(phoneNumber, appId);

                default:
                    return new OrderPhoneResponse { Description = "Order Phone feature is not support for " + vendor };
            }
        }

        public DeletePhoneResponse DeleteOrderPhoneNumber(VendorType vendor, string phoneNumberOrId)
        {
            return DeleteOrderPhoneNumberAsync(vendor, phoneNumberOrId).Result;
        }

        public async Task<DeletePhoneResponse> DeleteOrderPhoneNumberAsync(VendorType vendor, string id)
        {
            switch (vendor)
            {
                case VendorType.Plivo:
                    return await Plivo.DeleteOrderPhoneNumberAsync(id);

                case VendorType.Twilio:
                    return await Twilio.DeleteOrderPhoneNumberAsync(id);

                default:
                    return new DeletePhoneResponse { Description = "Delete Phone feature is not support for " + vendor };
            }
        }
    }
}
