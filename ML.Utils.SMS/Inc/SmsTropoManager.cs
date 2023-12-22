using ML.Utils.Phone;
using ML.Utils.Phone.Vendors.Tropo;
using System;
using System.Collections.Generic;
using Message = ML.Utils.Phone.Vendors.Twilio.Message;

namespace ML.Utils.SMS
{
	internal sealed class SmsTropoManager : SmsBaseManager
	{
		private readonly SmsTropoSetting _setting;

		public SmsTropoManager(SmsTropoSetting setting = null)
			: base(typeof(SmsTropoManager))
		{
			_setting = setting ?? SmsTropoSetting.LoadFromConfig();
		}

		public override SmsResult Send()
		{
			if (string.IsNullOrEmpty(FromPhone))
			{
				FromPhone = _setting.FromPhone;
			}

			PhoneManager.Tropo.Init(new TropoSetting
			{
				ApiUrl = _setting.ApiUrl,
				ApiVersion = _setting.ApiVersion,
				VoiceToken = _setting.VoiceToken,
				MessageToken = _setting.MessageToken,
				From = _setting.FromPhone
			});

			var response = PhoneManager.Tropo.SendSms(FromPhone, ToPhone, Message, log);

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
