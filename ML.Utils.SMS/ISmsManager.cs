using System;
using System.Collections.Generic;
using ML.Utils.Phone.Vendors.Twilio;

namespace ML.Utils.SMS
{
	public interface ISmsManager
	{
		/// <summary>
		/// Send SMS with SmsBaseSetting get from app config
		/// phone(10 digits numeric) - message(max length 160 chars)
		/// </summary>
		SmsResult SendSms(SmsProvider provider, string phone, string message);

		/// <summary>
        /// Send SMS with SmsBaseSetting get from app config
		/// phone(10 digits numeric) - message(max length 160 chars)
		/// </summary>
		SmsResult SendSms(SmsProvider provider, string fromPhone, string toPhone, string message);

        /// <summary>
        /// Send SMS with SmsBaseSetting get from data base
        /// phone(10 digits numeric) - message(max length 160 chars)
        /// </summary>
	    SmsResult SendSms(SmsProvider provider, SmsBaseSetting setting, string toPhone, string message);

	    List<Message> GetListMessage(SmsProvider provider, string from, string to, DateTime? dateSent);

        List<Message> GetListMessage(SmsProvider provider, SmsBaseSetting setting, string from, string to, DateTime? dateSent);
	}
}