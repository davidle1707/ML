using RestSharp;
using RestSharp.Extensions;
using RestSharp.Validation;
using System.Threading.Tasks;

namespace ML.Utils.Phone.Vendors.Twilio
{
    public partial class TwilioManager
    {
        /// <summary>
        /// Retrieve the details for an application instance. Makes a GET request to an Application Instance resource.
        /// </summary>
        /// <param name="applicationSid">The Sid of the application to retrieve</param>
        public virtual Application GetApplication(string applicationSid)
        {
            var request = new RestRequest();
            request.Resource = "Accounts/{AccountSid}/Applications/{ApplicationSid}";

            request.AddUrlSegment("ApplicationSid", applicationSid);

            return Execute<Application>(request);
        }

		/// <summary>
        /// List applications on current account with filters
        /// </summary>
        /// <param name="friendlyName">Optional friendly name to match</param>
        /// <param name="pageNumber">Page number to start retrieving results from</param>
        /// <param name="count">How many results to return</param>
        //[Obsolete("Use GetNextPage and GetPreviousPage for paging. Page parameter is scheduled for end of life https://www.twilio.com/engineering/2015/04/16/replacing-absolute-paging-with-relative-paging")]
        public Task<ApplicationResult> ListApplications(string friendlyName, int? pageNumber = null, int? count = null)
        {
            var request = new RestRequest {Resource = "Accounts/{AccountSid}/Applications"};

            if (friendlyName.HasValue()) request.AddParameter("FriendlyName", friendlyName);
            if (pageNumber.HasValue) request.AddParameter("Page", pageNumber.Value);
            if (count.HasValue) request.AddParameter("PageSize", count.Value);

            return ExecuteAsync<ApplicationResult>(request);
        }

        /// <summary>
        /// Create a new application
        /// </summary>
        /// <param name="options">Optional parameters to use when purchasing number</param>
        public virtual Task<Application> AddApplication(ApplicationOptions options)
        {
            Require.Argument("FriendlyName", options.FriendlyName);

            var request = new RestRequest(Method.POST) { Resource = "Accounts/{AccountSid}/Applications" };
			Core.AddApplicatiionOptionsToRequest(request, options);

            return ExecuteAsync<Application>(request);
        }

        public async Task<bool> UpdateApplicationSmsUrlAsync(string applicationSid, string smsUrl)
        {
            var response = await UpdateApplication(applicationSid, new ApplicationOptions { SmsUrl = smsUrl });

            return response != null;
        }

        /// <summary>
        /// Tries to update the application's properties, and returns the updated resource representation if successful.
        /// </summary>
        /// <param name="applicationSid">The Sid of the application to update</param>
        /// <param name="options">Which settings to update. Only properties with values set will be updated.</param>
        public Task<Application> UpdateApplication(string applicationSid, ApplicationOptions options)
        {
            Require.Argument("ApplicationSid", applicationSid);

            var request = new RestRequest(Method.POST) { Resource = "Accounts/{AccountSid}/Applications/{ApplicationSid}" };
			request.AddUrlSegment("ApplicationSid", applicationSid);

            Core.AddApplicatiionOptionsToRequest(request, options);

            return ExecuteAsync<Application>(request);
        }

        /// <summary>
        /// Delete this application. If this application's sid is assigned to any IncomingPhoneNumber resources as a VoiceApplicationSid or SmsApplicationSid it will be removed.
        /// </summary>
        /// <param name="applicationSid">The Sid of the number to remove</param>
        public async Task<DeleteStatus> DeleteApplication(string applicationSid)
        {
            Require.Argument("ApplicationSid", applicationSid);

            var request = new RestRequest(Method.DELETE) { Resource = "Accounts/{AccountSid}/Applications/{ApplicationSid}" };
            request.AddUrlSegment("ApplicationSid", applicationSid);

            var response = await _client.ExecuteTaskAsync(request);

            return response.StatusCode == System.Net.HttpStatusCode.NoContent ? DeleteStatus.Success : DeleteStatus.Failed;
        }
    }
}
