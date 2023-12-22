using ML.Utils.Phone;
using ML.Utils.Phone.Vendors.Twilio;
using System;
using System.Collections.Generic;

namespace ML.Utils.SMS
{
    internal sealed class SmsTwilioManager : SmsBaseManager
    {
        private readonly SmsTwilioSetting _setting;

        public SmsTwilioManager(SmsTwilioSetting setting = null)
            : base(typeof(SmsTwilioManager))
        {
			_setting = setting ?? SmsTwilioSetting.LoadFromConfig();
        }

        public override SmsResult Send()
        {
			if (string.IsNullOrEmpty(FromPhone))
			{
				FromPhone = _setting.FromPhone;
			}

			PhoneManager.Twilio.Init(new TwilioSetting
			{
				ApiUrl = _setting.ApiUrl,
				ApiVersion = _setting.ApiVersion,
				AccountSid = _setting.AccountSid,
				AuthToken = _setting.AuthToken,
				From = _setting.FromPhone
			});

	        var response = PhoneManager.Twilio.SendSms(FromPhone, ToPhone, Message, log);

			return new SmsResult
			{
				State = GetSmsState(response.Status),
				Description = response.Description,
				MessageId = response.ExternalSmsId,
				Price = response.ExternalSmsPrice
			};
        }

        public override List<Message> GetListMessage(string from, string to, DateTime? dateSent)
        {
			PhoneManager.Twilio.Init(new TwilioSetting
			{
				ApiUrl = _setting.ApiUrl,
				ApiVersion = _setting.ApiVersion,
				AccountSid = _setting.AccountSid,
				AuthToken = _setting.AuthToken,
				From = _setting.FromPhone
			});

            return PhoneManager.Twilio.GetListMessage(from, to, dateSent);
        }

    }
}
