
using ML.Common;
using ML.Utils.Phone.Vendors.Plivo;
using ML.Utils.Phone.Vendors.Tropo;
using ML.Utils.Phone.Vendors.Twilio;

namespace ML.Utils.SMS
{
	public abstract class SmsBaseSetting
	{
		public string FromPhone { get; set; }

		public string ApiUrl { get; set; }

		public string ApiVersion { get; set; }
	}

	public class SmsTwilioSetting : SmsBaseSetting
	{
		public string AccountSid { get; set; }

		public string AuthToken { get; set; }

		public static SmsTwilioSetting LoadFromConfig()
		{
			var setting = new TwilioSetting(true);

			return new SmsTwilioSetting
			       {
					   ApiUrl = setting.ApiUrl,
					   ApiVersion = setting.ApiVersion,
					   AccountSid = setting.AccountSid,
					   AuthToken = setting.AuthToken,
					   FromPhone = setting.From,
			       };
		}
	}

	public class SmsTropoSetting : SmsBaseSetting
	{
		public string MessageToken { get; set; }

		public string VoiceToken { get; set; }

		public static SmsTropoSetting LoadFromConfig()
		{
			var setting = new TropoSetting(true);

			return new SmsTropoSetting
			       {
					   ApiUrl = setting.ApiUrl,
					   ApiVersion = setting.ApiVersion,
					   VoiceToken = setting.VoiceToken,
					   MessageToken = setting.MessageToken,
					   FromPhone = setting.From,
			       };
		}
	}

	public class SmsPlivoSetting : SmsBaseSetting
	{
		public string AuthId { get; set; }

		public string AuthToken { get; set; }

		public static SmsPlivoSetting LoadFromConfig()
		{
			var setting = new PlivoSetting(true);

			return new SmsPlivoSetting
			{
				ApiUrl = setting.ApiUrl,
				ApiVersion = setting.ApiVersion,
				AuthId = setting.AuthId,
				AuthToken = setting.AuthToken,
				FromPhone = setting.From,
			};
		}
	}
}
