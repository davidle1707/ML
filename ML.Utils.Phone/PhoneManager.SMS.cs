using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ML.Utils.Phone
{
    public partial class PhoneManager
    {
        /// <summary>
        ///  Use From in vendor setting (Plivo, Twilio, Tropo)
        /// </summary>
        public SendSmsResponse SendSms(VendorType vendor, string to, string message)
        {
            return SendSmsAsync(vendor, to, message).Result;
        }

        /// <summary>
        /// Use From in vendor setting (Plivo, Twilio, Tropo)
        /// </summary>
        public Task<SendSmsResponse> SendSmsAsync(VendorType vendor, string to, string message)
        {
            var from = string.Empty;

            switch (vendor)
            {
                case VendorType.Plivo:
                    from = Plivo.Setting.From;
                    break;

                case VendorType.Twilio:
                    from = Twilio.Setting.From;
                    break;

                case VendorType.Tropo:
                    from = Tropo.Setting.From;
                    break;
            }

            return SendSmsAsync(vendor, from, to, message);
        }

        /// <summary>
        /// Vendor: Plivo, Twilio, Tropo
        /// </summary>
        public SendSmsResponse SendSms(VendorType vendor, string from, string to, string message)
        {
            return SendSmsAsync(vendor, from, to, message).Result;
        }

        /// <summary>
        /// Vendor: Plivo, Twilio, Tropo
        /// </summary>
        public async Task<SendSmsResponse> SendSmsAsync(VendorType vendor, string from, string to, string message)
        {
            var response = new SendSmsResponse { Status = SendSmsStatus.Error, Description = "Vendor is invalid." };

            switch (vendor)
            {
                case VendorType.Plivo:
                    response = await Plivo.SendSmsAsync(from, to, message, log);
                    response.FromPhone = Plivo.Setting.From;
                    response.FromVendor = VendorType.Plivo;
                    break;

                case VendorType.Twilio:
                    response = await Twilio.SendSmsAsync(from, to, message, log);
                    response.FromPhone = Twilio.Setting.From;
                    response.FromVendor = VendorType.Twilio;
                    break;

                case VendorType.Tropo:
                    response = await Tropo.SendSmsAsync(from, to, message, log);
                    response.FromPhone = Tropo.Setting.From;
                    response.FromVendor = VendorType.Tropo;
                    break;
            }

            return response;
        }

        public List<Vendors.Twilio.Message> GetListMessage(VendorType vendor, string from, string to, DateTime? dateSent)
        {
            switch (vendor)
            {
                //case VendorType.Plivo:
                //    return Plivo.SendSms(from, to, message, log);

                case VendorType.Twilio:
                    return Twilio.GetListMessage(from, to, dateSent);

                //case VendorType.Tropo:
                //    return Tropo.SendSms(from, to, message, log);

                default:
                    return new List<Vendors.Twilio.Message>();
            }
        }

        public async Task<bool> ChangeApplicationSmsUrlAsync(VendorType vendor, string appId, string smsCallbackUrl)
        {
            switch (vendor)
            {
                case VendorType.Plivo:
                   return await Plivo.ModifyApplicationSmsUrlAsync(appId, smsCallbackUrl);

                case VendorType.Twilio:
                    return await Twilio.UpdateApplicationSmsUrlAsync(appId, smsCallbackUrl);

                default:
                    return false;
            }
        }
    }
}
