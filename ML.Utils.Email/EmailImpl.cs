using ML.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading;

namespace ML.Utils.Email
{
    public class EmailImpl : IEmail
    {
        private readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(EmailImpl));

        public void SendEmail(MailMessage message, SmtpSetting smtpSetting = null, bool useEmbeddedResource = false, string embeddedResourcePath = null, Action<Exception> error = null, Action<bool> final = null)
        {
            var smtp = smtpSetting ?? GetSmtpSettingFromAppConfig();

            if (string.IsNullOrEmpty(message.From?.Address))
            {
                message.From = new MailAddress((string.IsNullOrWhiteSpace(smtp.From) ? smtp.UserName : smtp.From), smtp.FullName);
            }

            if (message.Sender == null)
            {
                message.Sender = message.From;
            }

            if (!string.IsNullOrWhiteSpace(smtp.SubAccount))
            {
                message.Headers.Add("X-MC-Subaccount", smtp.SubAccount);
            }

            if (!string.IsNullOrWhiteSpace(smtp.ViaDomain))
            {
                message.Headers.Add("X-MC-SigningDomain", smtp.ViaDomain);
            }

            if (useEmbeddedResource && !string.IsNullOrEmpty(embeddedResourcePath))
            {
                MapEmbeddedResource(message, embeddedResourcePath);
            }

            var thread = new Thread(SendEmailbyThreadAsync) { Priority = ThreadPriority.BelowNormal };
            thread.Start(new ArrayList { message, smtp, error, final });
        }

        private void SendEmailbyThreadAsync(object tparams)
        {
            var success = true;

            var @params = (ArrayList)tparams;
            var message = @params[0] as MailMessage;
            var smptSetting = @params[1] as SmtpSetting;
            var error = @params[2] as Action<Exception>;
            var final = @params[3] as Action<bool>;

            try
            {
                if (message == null || smptSetting == null )
                {
                    return;
                }
                
                var client = new SmtpClient(smptSetting.Server)
                {
                    EnableSsl = smptSetting.EnableSsl,
                    Port = smptSetting.Port,
                    Credentials = new NetworkCredential(smptSetting.UserName, smptSetting.Password)
                };
                
                client.Send(message);
            }
            catch (Exception ex)
            {
                _log.Info("Unable to send email", ex);

                success = false;
                
                error?.Invoke(ex);
            }
            finally
            {
                final?.Invoke(success);
            }
        }

        public void SendEmailNoTheard(MailMessage message, SmtpSetting smtpSetting = null, bool throwError = false)
        {
            var smtp = smtpSetting ?? GetSmtpSettingFromAppConfig();

            try
            {
                var client = new SmtpClient(smtp.Server)
                {
                    EnableSsl = smtp.EnableSsl,
                    UseDefaultCredentials = false,
                    Credentials = new System.Net.NetworkCredential(smtp.UserName, smtp.Password),
                    Port = smtp.Port
                };

                message.Sender = message.From;

                client.Send(message);
            }
            catch (Exception ex)
            {
                _log.Info("Unable to send email", ex);
                if (throwError)
                {
                    throw;
                }
            }
        }

        private void MapEmbeddedResource(MailMessage message, string resourcePath)
        {
            var matchImgs = Regex.Matches(message.Body, "<img.+?src=[\"'](.+?)[\"'].*?>", RegexOptions.IgnoreCase);

            if (matchImgs.Count == 0) return;

            var linkResources = new List<LinkedResource>();
            for (var i = 0; i < matchImgs.Count; i++)
            {
                if (matchImgs[i].Groups.Count <= 1) continue;

                var id = Guid.NewGuid().ToString();

                var img = matchImgs[i].Value.Replace(matchImgs[i].Groups[1].Value, $"cid:{id}");
                message.Body = message.Body.Replace(matchImgs[i].Value, img);

                var imagePath = Path.Combine(resourcePath, matchImgs[i].Groups[1].Value.TrimStart('/'));

                if (File.Exists(imagePath))
                {
                    linkResources.Add(new LinkedResource(imagePath) { ContentId = id });
                }
            }

            var htmlView = AlternateView.CreateAlternateViewFromString(message.Body, null, "text/html");
            foreach (var linkedResource in linkResources)
            {
                htmlView.LinkedResources.Add(linkedResource);
            }

            message.IsBodyHtml = true;
            message.AlternateViews.Add(htmlView);
        }

        public SmtpSetting GetSmtpSettingFromAppConfig(string prefixAppKey = "SMTP_")
        {
            return new SmtpSetting
            {
                EnableSsl = std.AppSettings[prefixAppKey + "REQUIRES_SSL"].ToBool(),
                UserName = std.AppSettings[prefixAppKey + "LOGIN"].ToStr(),
                Password = std.AppSettings[prefixAppKey + "PASSWORD"].ToStr(),
                Port = std.AppSettings[prefixAppKey + "PORT"].ToInt(),
                Server = std.AppSettings[prefixAppKey + "SERVER"].ToStr(),
                FullName = std.AppSettings[prefixAppKey + "FULLNAME"].ToStr(),
                SubAccount = std.AppSettings[prefixAppKey + "SUBACCOUNT"].ToStr(),
                From = std.AppSettings[prefixAppKey + "FROM"].ToStr(),
                ViaDomain = std.AppSettings[prefixAppKey + "VIADOMAIN"].ToStr()
            };
        }

    }
}
