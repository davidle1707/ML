using System;
using System.Net.Mail;

namespace ML.Utils.Email
{
    public interface IEmail
	{
	    /// <summary>
	    /// if smtpSetting is null, use SmtpSetting from app config
	    /// </summary>
        void SendEmail(MailMessage message, SmtpSetting smtpSetting = null, bool useEmbeddedResource = false, string embeddedResourcePath = null, Action<Exception> error = null, Action<bool> final = null);

		/// <summary>
		/// if smtpSetting is null, use SmtpSetting from app config
		/// </summary>
		void SendEmailNoTheard(MailMessage message, SmtpSetting smtpSetting = null, bool throwError = false);

		SmtpSetting GetSmtpSettingFromAppConfig(string prefixAppKey = "SMTP_");
	}
}