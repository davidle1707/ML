
using ML.Common;

namespace ML.Utils.Phone.Vendors.Twilio
{
    /// <summary>
    /// Url (https://api.twilio.com) Version (2010-04-01)
    /// </summary>
    public class TwilioSetting : VendorSetting
    {
        ///// Default: Url https://api.twilio.com
        public TwilioSetting(bool fromAppSetting = false)
		{
			ApiUrl = "https://api.twilio.com";

			ApiVersion = "2010-04-01";

			if (fromAppSetting)
			{
				ApiUrl = std.AppSettings["TWILIO_URL"].ToStr();
				ApiVersion = std.AppSettings["TWILIO_API_VERSION"].ToStr();
				AccountSid = std.AppSettings["TWILIO_ACCOUNT_SID"].ToStr();
				AuthToken = std.AppSettings["TWILIO_AUTH_TOKEN"].ToStr();
				From = std.AppSettings["TWILIO_API_FROM"].ToStr();
			}
		}
        public string AccountSid { get; set; }

	    public override bool IsValid()
	    {
		    return !string.IsNullOrWhiteSpace(ApiUrl) && !string.IsNullOrWhiteSpace(ApiVersion)
		           && !string.IsNullOrWhiteSpace(AccountSid) && !string.IsNullOrWhiteSpace(AuthToken);
	    }

        public override VendorType Type => VendorType.Twilio;
    }
}
