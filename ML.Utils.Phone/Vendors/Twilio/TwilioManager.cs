using RestSharp;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ML.Utils.Phone.Vendors.Twilio
{
    /*
		Possible POST or PUT Response Status Codes
			200 OK: The request was successful, we updated the resource and the response body contains the representation.
			201 CREATED: The request was successful, we created a new resource and the response body contains the representation.
			400 BAD REQUEST: The data given in the POST or PUT failed validation. Inspect the response body for details.
			401 UNAUTHORIZED: The supplied credentials, if any, are not sufficient to create or update the resource.
			404 NOT FOUND: You know this one.
			405 METHOD NOT ALLOWED: You can't POST or PUT to the resource.
			500 SERVER ERROR: We couldn't create or update the resource. Please try again.

        https://github.com/twilio/twilio-csharp/tree/master/src/Twilio.Api
	*/
    public partial class TwilioManager
	{
		#region Fields

		private RestClient _client;

		public TwilioSetting Setting { get; private set; }

		#endregion

		public TwilioManager()
		{
		}

		public TwilioManager(bool fromAppSetting)
			: this()
		{
			var setting = new TwilioSetting(fromAppSetting);

			Init(setting);
		}

		public TwilioManager(TwilioSetting setting)
			: this()
		{
			Init(setting);
		}

		public void Init(TwilioSetting setting)
		{
			Setting = setting ?? new TwilioSetting();

			if (Setting == null || !Setting.IsValid())
			{
				throw new TwilioException("Twilio setting is invalid.");
			}

			_client = new RestClient
			{
				Authenticator = new HttpBasicAuthenticator(Setting.AccountSid, Setting.AuthToken),
				BaseUrl = new Uri($"{Setting.ApiUrl}/{Setting.ApiVersion}")
			};

			// if acting on a subaccount, use request.AddUrlSegment("AccountSid", "value")
			// to override for that request.
			_client.AddDefaultUrlSegment("AccountSid", Setting.AccountSid);
		}

		private T Execute<T>(RestRequest request) where T : new()
		{
			if (!Setting.IsValid())
			{
				throw new TwilioException("Twilio setting is invalid.");
			}

			request.OnBeforeDeserialization = (resp) =>
			{
				// for individual resources when there's an error to make
				// sure that RestException props are populated
				if (((int)resp.StatusCode) >= 400)
				{
					request.RootElement = "";
				}
			};

			request.DateFormat = "ddd, dd MMM yyyy HH:mm:ss '+0000'";

			var response = _client.Execute<T>(request);

			return response.Data;
		}

        private async Task<T> ExecuteAsync<T>(RestRequest request) where T : new()
        {
            if (!Setting.IsValid())
            {
                throw new TwilioException("Twilio setting is invalid.");
            }

            request.OnBeforeDeserialization = resp =>
            {
                // for individual resources when there's an error to make
                // sure that RestException props are populated
                if ((int)resp.StatusCode >= 400)
                {
                    request.RootElement = "";
                }
            };

            request.DateFormat = "ddd, dd MMM yyyy HH:mm:ss '+0000'";

            var response = await _client.ExecuteTaskAsync<T>(request);

            return response.Data;

            //return response.StatusCode == HttpStatusCode.Created || response.StatusCode == HttpStatusCode.OK ? response.Data : default(T);
        }
	}
}
