using System;
using System.Collections.Generic;
using ML.Common;
using ML.Utils.Phone.Vendors.Twilio;

namespace ML.Utils.SMS
{
	public class SmsManager : ISmsManager
	{
		public SmsResult SendSms(SmsProvider provider, string toPhone, string message)
		{
			var setting = GetSmsSetting(provider);

			return SendSms(provider, setting, toPhone, message);
		}

		public SmsResult SendSms(SmsProvider provider, string fromPhone, string toPhone, string message)
		{
			var setting = GetSmsSetting(provider);

			if (setting != null)
			{
				setting.FromPhone = fromPhone;
			}

			return SendSms(provider, setting, toPhone, message);
		}
		
		public SmsResult SendSms(SmsProvider provider, SmsBaseSetting setting, string toPhone, string message)
		{
			if (setting == null)
			{
				return new SmsResult { State = SmsState.Unsuccess, Description = "SMS setting is not yet initialized." };
			}

			if (toPhone.ToPhoneNumber() == "")
			{
				return new SmsResult { State = SmsState.Unsuccess, Description = "Phone number is invalid." };
			}

			if (message.Length > 160)
			{
				return new SmsResult { State = SmsState.Unsuccess, Description = "Message cannot longer than 160 characters." };
			}

			if (provider == SmsProvider.Twilio && !(setting is SmsTwilioSetting))
			{
				return new SmsResult { State = SmsState.Unsuccess, Description = "SMS setting is invalid." };
			}

			if (provider == SmsProvider.Tropo && !(setting is SmsTropoSetting))
			{
				return new SmsResult { State = SmsState.Unsuccess, Description = "SMS setting is invalid." };
			}

			if (provider == SmsProvider.Plivo && !(setting is SmsPlivoSetting))
			{
				return new SmsResult { State = SmsState.Unsuccess, Description = "SMS setting is invalid." };
			}

			var smsManager = GetSmsManager(provider, setting);

			if (smsManager == null)
			{
				return new SmsResult {State = SmsState.Unsuccess, Description = "Unable to find provider to send SMS"};
			}

			smsManager.FromPhone = setting.FromPhone;
			smsManager.ToPhone = toPhone;
			smsManager.Message = message;

			return smsManager.Send();
		}

        public List<Message> GetListMessage(SmsProvider provider, string from, string to, DateTime? dateSent)
        {
            var setting = GetSmsSetting(provider);

            var smsManager = GetSmsManager(provider,  setting);

            if (smsManager != null)
            {
                return smsManager.GetListMessage(from, to, dateSent);
            }

            return new List<Message>();
        }

        public List<Message> GetListMessage(SmsProvider provider, SmsBaseSetting setting, string from, string to, DateTime? dateSent)
        {
            if (setting == null)
            {
                return new List<Message>();
            }

            var smsManager = GetSmsManager(provider, setting);

            if (smsManager != null)
            {
                return smsManager.GetListMessage(from, to, dateSent);
            }
            return new List<Message>();
        }
		
		private SmsBaseSetting GetSmsSetting(SmsProvider provider)
		{
			switch (provider)
			{
				case SmsProvider.Twilio:
					return SmsTwilioSetting.LoadFromConfig();

				case SmsProvider.Tropo:
					return SmsTropoSetting.LoadFromConfig();

				case SmsProvider.Plivo:
					return SmsPlivoSetting.LoadFromConfig();
			}

			return null;
		}

		private SmsBaseManager GetSmsManager(SmsProvider provider, SmsBaseSetting setting)
		{
			switch (provider)
			{
				case SmsProvider.Twilio:
					return new SmsTwilioManager((SmsTwilioSetting)setting);

				case SmsProvider.Tropo:
					return new SmsTropoManager((SmsTropoSetting)setting);

				case SmsProvider.Plivo:
					return new SmsPlivoManager((SmsPlivoSetting)setting);
			}

			return null;
		}
	}
}
