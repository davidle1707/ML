using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using ML.Utils.Phone.Vendors.Vitelity.Base;
using ML.Utils.Phone.Vendors.Vitelity.Models;

namespace ML.Utils.Phone.Vendors.Vitelity
{
    public class VitelityManager : VendorManager
    {
	    public VitelitySetting Setting { get; private set; }
    
        public VitelityManager()
        {
		}

		public VitelityManager(bool fromAppSetting)
			: this()
		{
			Init(new VitelitySetting(fromAppSetting));
		}

        public VitelityManager(VitelitySetting setting)
			: this()
        {
            Init(setting);
        }

        public void Init(VitelitySetting setting)
        {
            Setting = setting;

			if (Setting == null || !Setting.IsValid())
			{
				throw new VitelityException("Vitelity setting is invalid.");
			}
        }

        public SendFaxResponse SendFax(SendFaxRequest request)
        {
            var result = new SendFaxResponse { Success = false };

            var response = PostRequest(request);
            result.Parse(response);

            return result;
        }

        public GetFaxResponse GetFax(GetFaxRequest request)
        {
            var result = new GetFaxResponse { Success = false };

            var response = PostRequest(request);
            result.Parse(response);

            return result;
        }

        public ListInComingFaxesResponse ListInComingFaxes(ListInComingFaxesRequest request)
        {
            var result = new ListInComingFaxesResponse { Success = false };

            var response = PostRequest(request);
            result.Parse(response);

            return result;
        }

        public SentFaxStatusResponse SentFaxStatus(SentFaxStatusRequest request)
        {
            var result = new SentFaxStatusResponse { Success = false };

            var response = PostRequest(request);
            result.Parse(response);

            return result;
        }

        public FaxListDidsResponse FaxListDids(FaxListDidsRequest request)
        {
            var result = new FaxListDidsResponse { Success = false };

            var response = PostRequest(request);
            result.Parse(response);

            return result;
        }

        public FaxGetDidResponse FaxGetDid(FaxGetDidRequest request)
        {
            var result = new FaxGetDidResponse { Success = false };

            var response = PostRequest(request);
            result.Parse(response);

            return result;
        }

        public FaxListMyDidsResponse FaxListMyDids(FaxListMyDidsRequest request)
        {
            var result = new FaxListMyDidsResponse { Success = false };

            var response = PostRequest(request);
            result.Parse(response);

            return result;
        }

        public FaxListStatesResponse FaxListStates(FaxListStatesRequest request)
        {
            var result = new FaxListStatesResponse { Success = false };

            var response = PostRequest(request);
            result.Parse(response);

            return result;
        }

        public FaxChangeEmailResponse FaxChangeEmail(FaxChangeEmailRequest request)
        {
            var result = new FaxChangeEmailResponse { Success = false };

            var response = PostRequest(request);
            result.Parse(response);

            return result;
        }

        private VitelityResponseXml PostRequest(BaseVitelityRequest request)
        {
			if (!Setting.IsValid())
			{
				throw new VitelityException("Vitelity setting is invalid.");
			}

            var mainUrl = GetBaseUrl(request.CmdName);
            var postData = Encoding.UTF8.GetBytes(request.QueryString);

            var responseText = ProcessPostRequest(mainUrl, postData);

            if (!string.IsNullOrEmpty(responseText) && responseText.ToLower().Contains("unauthorized access"))
            {
                mainUrl = GetBaseUrl("faxgetdid");
                responseText = ProcessPostRequest(mainUrl);

                throw new VitelityException(responseText, true);
            }

            return new VitelityResponseXml(responseText);
        }

        private string ProcessPostRequest(string url, byte[] postData = null)
        {
            var webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Method = "POST";
            webRequest.ContentType = "application/x-www-form-urlencoded";

            if (postData != null && postData.Length > 0)
            {
                var webRequestStream = webRequest.GetRequestStream();
                webRequestStream.Write(postData, 0, postData.Length);
                webRequestStream.Close();
            }

            string responseText;

            var webResponse = webRequest.GetResponse();

            using (var dataStream = webResponse.GetResponseStream())
            {
                var sr = new StreamReader(dataStream, Encoding.UTF8);
                responseText = sr.ReadToEnd();
                sr.Close();
            }

            return responseText;
        }

        private string GetBaseUrl(string commandName)
        {
            return $"{Setting.ApiUrl}?login={Setting.Login}&pass={Setting.Password}&cmd={commandName}";
        }


    }
}
