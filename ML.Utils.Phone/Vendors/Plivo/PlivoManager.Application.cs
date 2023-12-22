using ML.Utils.Phone.Vendors.Plivo.Models;
using RestSharp;
using System;
using System.Threading.Tasks;
using dict = System.Collections.Generic.Dictionary<string, string>;

namespace ML.Utils.Phone.Vendors.Plivo
{
    public partial class PlivoManager
    {
        public IRestResponse<ApplicationList> GetApplications()
        {
            return ExecuteRequest<ApplicationList>("GET", "/Application/", new dict());
        }

        public IRestResponse<ApplicationList> GetApplications(dict parameters)
        {
            return ExecuteRequest<ApplicationList>("GET", "/Application/", parameters);
        }

        public IRestResponse<Application> GetApplication(dict parameters)
        {
            var app_id = GetValueAndRemove(ref parameters, "app_id");
            Console.WriteLine("application");
            return ExecuteRequest<Application>("GET", $"/Application/{app_id}/", parameters);

        }

        public IRestResponse<BaseResponse> CreateApplication(dict parameters)
        {
            return ExecuteRequest<BaseResponse>("POST", "/Application/", parameters);
        }

        public async Task<bool> ModifyApplicationSmsUrlAsync(string appId, string messageUrl)
        {
            var response = await ModifyApplication(new dict
            {
                ["app_id"] = appId,
                ["message_url"] = messageUrl
            });

            return response?.Data?.message == "changed";
        }

        public Task<IRestResponse<BaseResponse>> ModifyApplication(dict parameters)
        {
            var app_id = GetValueAndRemove(ref parameters, "app_id");
            return ExecuteRequestAsync<BaseResponse>("POST", $"/Application/{app_id}/", parameters);
        }

        public IRestResponse<BaseResponse> DeleteApplication(dict parameters)
        {
            var app_id = GetValueAndRemove(ref parameters, "app_id");
            return ExecuteRequest<BaseResponse>("DELETE", $"/Application/{app_id}/", parameters);
        }
    }
}
