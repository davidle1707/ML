using log4net;
using ML.Common;
using ML.Utils.Phone.Vendors.Tropo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace ML.Utils.Phone
{
    public static partial class PhoneExtensions
    {
        public static SendSmsResponse SendSms(this TropoManager manager, string to, string message, ILog log = null)
        {
            return manager.SendSmsAsync(manager.Setting.From, to, message, log).Result;
        }

        public static Task<SendSmsResponse> SendSmsAsync(this TropoManager manager, string to, string message, ILog log = null)
        {
            return manager.SendSmsAsync(manager.Setting.From, to, message, log);
        }

        public static SendSmsResponse SendSms(this TropoManager manager, string from, string to, string message, ILog log = null)
        {
            return manager.SendSmsAsync(from, to, message, log).Result;
        }

        public static async Task<SendSmsResponse> SendSmsAsync(this TropoManager manager, string from, string to, string message, ILog log = null)
        {
            var response = new SendSmsResponse { Status = SendSmsStatus.Unsuccess };

            try
            {
                var parameters = new Dictionary<string, string>
                                 {
                                     {"from", from},
                                     {"to", to},
                                     {"channel", Channel.Text},
                                     {"network", Network.SMS},
                                     {"msg", HttpUtility.UrlEncode(message)}
                                 };

                var smsStream = await TropoManager.ExecuteMessageRequestAsync(manager.Setting, parameters);

                // Create an XML doc to hold the response from the Tropo Session API.
                var doc = new XmlDocument();
                doc.Load(smsStream);

                var nodeSession = doc.SelectSingleNode("session");
                if (nodeSession != null)
                {
                    response.ExternalSmsJson = JsonConvert.SerializeXmlNode(nodeSession, Newtonsoft.Json.Formatting.None);

                    var nodeId = nodeSession.SelectSingleNode("id");
                    response.ExternalSmsId = nodeId != null ? nodeId.InnerText.ToStr() : string.Empty;

                    var nodeSuccess = nodeSession.SelectSingleNode("success");
                    response.Status = nodeSuccess != null && nodeSuccess.InnerText.EqualsString("true") ? SendSmsStatus.Success : SendSmsStatus.Unsuccess;

                    var tokenSuccess = nodeSession.SelectSingleNode("token");
                    response.Description = tokenSuccess != null ? tokenSuccess.InnerText.ToStr() : string.Empty;
                }
            }
            catch (Exception ex)
            {
                response.Status = SendSmsStatus.Error;
                response.Description = ex.Message;

                log?.Error("Error when sending sms by Tropo", ex);
            }

            return response;
        }

    }
}
