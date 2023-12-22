using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ML.Utils.PushService.PushWoosh
{
    public class PushWooshProcessor
    {
        private readonly PushWooshSetting _setting;

        public PushWooshProcessor(PushWooshSetting setting)
        {
            _setting = setting;
        }

        public PushWooshResponse CreateMessage(CreateMessageRequest request)
        {
            var json = new JObject(
                new JProperty("application", _setting.ApplicationCode),
                new JProperty("auth", _setting.ApiAccessToken),
                new JProperty("notifications",
                    new JArray(
                        new JObject(
                            new JProperty("send_date", request.SendDate),
                            new JProperty("ignore_user_timezone", request.IgnoreUserTimezone),
                            new JProperty("content", request.Content),
                            new JProperty("timezone", request.TimeZone)
                            ))));

            return ExcuteRequest("createMessage", json);
        }

        public PushWooshResponse DeleteMessage(DeleteMessageRequest request)
        {
            var json = new JObject(
                new JProperty("auth", _setting.ApiAccessToken),
                new JProperty("message", request.MessageCode)
                );

            return ExcuteRequest("deleteMessage", json);
        }


        #region private function

        private PushWooshResponse ExcuteRequest(string action, JObject data)
        {
            var url = new Uri(string.Format("{0}/{1}", _setting.ServiceUrl.TrimEnd('/'), action));
            var json = new JObject(new JProperty("request", data));

            var req = (HttpWebRequest)WebRequest.Create(url);
            req.ContentType = "text/json";
            req.Method = "POST";

            using (var streamWriter = new StreamWriter(req.GetRequestStream()))
            {
                streamWriter.Write(json.ToString());
            }

            HttpWebResponse httpResponse;

            try
            {
                httpResponse = (HttpWebResponse)req.GetResponse();
            }
            catch (Exception exc)
            {
                throw new Exception(string.Format("Problem with {0}, {1}", url, exc.Message));
            }

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var responseText = streamReader.ReadToEnd();
                return JsonConvert.DeserializeObject<PushWooshResponse>(responseText);
            }
        }

        #endregion
    }
}
