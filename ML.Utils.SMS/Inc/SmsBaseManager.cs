using ML.Utils.Phone;
using ML.Utils.Phone.Vendors.Twilio;
using System;
using System.Collections.Generic;

namespace ML.Utils.SMS
{
	internal abstract class SmsBaseManager
	{
		protected log4net.ILog log = null;

		public readonly PhoneManager PhoneManager;

		protected SmsBaseManager(Type inheritedClassType = null)
		{
			FromPhone = "";
			ToPhone = "";
			Message = "";
			log = log4net.LogManager.GetLogger(inheritedClassType ?? typeof(SmsBaseManager));

			PhoneManager = new PhoneManager();
		}

		public string FromPhone { get; set; }

		public string ToPhone { get; set; }

		public string Message { get; set; }

		public abstract SmsResult Send();

	    public abstract List<Message> GetListMessage(string from, string to, DateTime? dateSent);

		protected  SmsState GetSmsState(SendSmsStatus sendStatus)
	    {
		    switch (sendStatus)
		    {
				 case SendSmsStatus.Success:
					return SmsState.Success;

				 case SendSmsStatus.Unsuccess:
					return SmsState.Unsuccess;

				default:
					return SmsState.Error;
		    }
	    }
	}
}
