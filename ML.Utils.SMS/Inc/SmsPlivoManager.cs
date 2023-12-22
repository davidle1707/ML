using ML.Utils.Phone;
using ML.Utils.Phone.Vendors.Plivo;
using ML.Utils.Phone.Vendors.Twilio;
using System;
using System.Collections.Generic;

namespace ML.Utils.SMS
{
    internal sealed class SmsPlivoManager : SmsBaseManager
    {
        private readonly SmsPlivoSetting _setting;

		public SmsPlivoManager(SmsPlivoSetting setting = null)
            : base(typeof(SmsPlivoManager))
        {
			_setting = setting ?? SmsPlivoSetting.LoadFromConfig();
        }

        public override SmsResult Send()
        {
			if (string.IsNullOrEmpty(FromPhone))
			{
				FromPhone = _setting.FromPhone;
			}

			PhoneManager.Plivo.Init(new PlivoSetting
			{
				ApiUrl = _setting.ApiUrl,
				ApiVersion = _setting.ApiVersion,
				AuthId = _setting.AuthId,
				AuthToken = _setting.AuthToken,
				From = _setting.FromPhone
			});

			var response = PhoneManager.Plivo.SendSms(FromPhone, ToPhone, Message, log);

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
			return new List<Message>();
        }

    }
}
