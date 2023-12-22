using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dict = System.Collections.Generic.Dictionary<string, string>;


namespace ML.Utils.Phone.Vendors.Plivo
{
    /// <summary>
    /// https://www.plivo.com/docs/api/
    /// </summary>
    public partial class PlivoManager : VendorManager
    {
        private RestClient _client;

        public PlivoSetting Setting { get; private set; }

        public PlivoManager()
        {
        }

        public PlivoManager(bool fromAppSetting)
            : this()
        {
            Init(new PlivoSetting(fromAppSetting));
        }

        public PlivoManager(PlivoSetting setting)
            : this()
        {
            Init(setting);
        }

        public void Init(PlivoSetting setting)
        {
            Setting = setting ?? new PlivoSetting();

            if (Setting == null && !Setting.IsValid())
            {
                throw new PlivoException("Plivo setting is invalid.");
            }

            _client = new RestClient
                      {
                          Authenticator = new HttpBasicAuthenticator(Setting.AuthId, Setting.AuthToken),
                          BaseUrl = new Uri($"{Setting.ApiUrl}/{Setting.ApiVersion}/Account/{Setting.AuthId}"),
                          UserAgent = "PlivoCsharp",
                      };
        }

        private IRestResponse<T> ExecuteRequest<T>(string httpMethod, string resource, dict data) where T : new()
        {
            return ExecuteRequestAsync<T>(httpMethod, resource, data).Result;
        }

        private async Task<IRestResponse<T>> ExecuteRequestAsync<T>(string httpMethod, string resource, dict data) where T : new()
        {
            if (!Setting.IsValid())
            {
                throw new PlivoException("Plivo setting is invalid.");
            }

            var request = new RestRequest { Resource = resource, RequestFormat = DataFormat.Json };

            // add the parameters to the request
            foreach (var kvp in data)
            {
                request.AddParameter(kvp.Key, Util.HtmlConvert(kvp.Value), ParameterType.QueryString);
                //Console.Write("{0} - {1}", kvp.Key, kvp.Value);
            }

            //set the HTTP method for this request
            switch (httpMethod.ToUpper())
            {
                case "GET": request.Method = Method.GET;
                    break;

                case "POST": request.Method = Method.POST;
                    request.Parameters.Clear();
                    request.AddParameter("application/json", request.JsonSerializer.Serialize(data), ParameterType.RequestBody);
                    break;

                case "DELETE": request.Method = Method.DELETE;
                    break;

                default: request.Method = Method.GET;
                    break;
            }

            _client.AddHandler("application/json", new JsonDeserializer());

            var response = await _client.ExecuteTaskAsync<T>(request);

            return response;
        }

        private string GetValueAndRemove(ref dict dict, string key)
        {
            var value = "";
            try
            {
                value = dict[key];
                dict.Remove(key);
            }
            catch (KeyNotFoundException)
            {
                throw new PlivoException($"Missing mandatory parameter {key}.");
            }
            return value;
        }
    }
}
