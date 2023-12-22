using RestSharp;
using RestSharp.Extensions;
using RestSharp.Validation;
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
    */
    public partial class TwilioManager
    {
        #region AVAILABLE PHONE NUMBERS

        /// <summary>
        /// Search available local phone numbers. Makes a GET request to the AvailablePhoneNumber List resource.
        /// </summary>
        /// <param name="isoCountryCode">Two-character ISO country code (US or CA)</param>
        /// <param name="options">Search filter options. Only properties with values set will be used.</param>
        public AvailablePhoneNumberResult ListAvailableLocalPhoneNumbers(string isoCountryCode, AvailablePhoneNumberListRequest options)
        {
            return ListAvailableLocalPhoneNumbersAsync(isoCountryCode, options).Result;
        }

        /// <summary>
        /// Search available local phone numbers. Makes a GET request to the AvailablePhoneNumber List resource.
        /// </summary>
        /// <param name="isoCountryCode">Two-character ISO country code (US or CA)</param>
        /// <param name="options">Search filter options. Only properties with values set will be used.</param>
        public Task<AvailablePhoneNumberResult> ListAvailableLocalPhoneNumbersAsync(string isoCountryCode, AvailablePhoneNumberListRequest options)
        {
            Require.Argument("isoCountryCode", isoCountryCode);

            var request = new RestRequest { Resource = "Accounts/{AccountSid}/AvailablePhoneNumbers/{IsoCountryCode}/Local.json" };
            request.AddUrlSegment("IsoCountryCode", isoCountryCode);

            Core.AddNumberSearchParameters(options, request);

            return ExecuteAsync<AvailablePhoneNumberResult>(request);
        }

        /// <summary>
        /// Search available toll-free phone numbers.  Makes a GET request to the AvailablePhoneNumber List resource.
        /// </summary>
        /// <param name="isoCountryCode">Two-character ISO country code (US or CA)</param>
        /// <param name="options">Search filter options. Only properties with values set will be used.</param>
        public AvailablePhoneNumberResult ListAvailableTollFreePhoneNumbers(string isoCountryCode, AvailablePhoneNumberListRequest options)
        {
            return ListAvailableTollFreePhoneNumbersAsync(isoCountryCode, options).Result;
        }

        /// <summary>
        /// Search available toll-free phone numbers.  Makes a GET request to the AvailablePhoneNumber List resource.
        /// </summary>
        /// <param name="isoCountryCode">Two-character ISO country code (US or CA)</param>
        /// <param name="options">Search filter options. Only properties with values set will be used.</param>
        public Task<AvailablePhoneNumberResult> ListAvailableTollFreePhoneNumbersAsync(string isoCountryCode, AvailablePhoneNumberListRequest options)
        {
            Require.Argument("isoCountryCode", isoCountryCode);
            //Require.Argument("contains", contains);

            var request = new RestRequest { Resource = "Accounts/{AccountSid}/AvailablePhoneNumbers/{IsoCountryCode}/TollFree.json" };
            request.AddUrlSegment("IsoCountryCode", isoCountryCode);
            
            Core.AddNumberSearchParameters(options, request);

            return ExecuteAsync<AvailablePhoneNumberResult>(request);
        }

        #endregion

        #region INCOMMING PHONE NUMBERS

        /// <summary>
        /// Retrieve the details for an incoming phone number
        /// </summary>
        /// <param name="incomingPhoneNumberSid">The Sid of the number to retrieve</param>
        public IncomingPhoneNumber GetIncomingPhoneNumber(string incomingPhoneNumberSid)
        {
            var request = new RestRequest  { Resource = "Accounts/{AccountSid}/IncomingPhoneNumbers/{IncomingPhoneNumberSid}.json" };
            request.AddUrlSegment("IncomingPhoneNumberSid", incomingPhoneNumberSid);
            
            return Execute<IncomingPhoneNumber>(request);
        }

        /// <summary>
        /// List all incoming phone numbers on current account
        /// </summary>
        public IncomingPhoneNumberResult ListIncomingPhoneNumbers()
        {
            return ListIncomingPhoneNumbers(null, null, null, null);
        }

        /// <summary>
        /// List incoming phone numbers on current account with filters
        /// </summary>
        /// <param name="phoneNumber">Optional phone number to match</param>
        /// <param name="friendlyName">Optional friendly name to match</param>
        /// <param name="pageNumber">Page number to start retrieving results from</param>
        /// <param name="count">How many results to return</param>
        public IncomingPhoneNumberResult ListIncomingPhoneNumbers(string phoneNumber, string friendlyName, int? pageNumber, int? count)
        {
            var request = new RestRequest();
            request.Resource = "Accounts/{AccountSid}/IncomingPhoneNumbers.json";

            if (phoneNumber.HasValue()) request.AddParameter("PhoneNumber", phoneNumber);
            if (friendlyName.HasValue()) request.AddParameter("FriendlyName", friendlyName);

            if (pageNumber.HasValue) request.AddParameter("Page", pageNumber.Value);
            if (count.HasValue) request.AddParameter("PageSize", count.Value);

            return Execute<IncomingPhoneNumberResult>(request);
        }

        /// <summary>
        /// Purchase/provision a local phone number
        /// </summary>
        /// <param name="options">Optional parameters to use when purchasing number</param>
        public IncomingPhoneNumber AddIncomingPhoneNumber(PhoneNumberOptions options)
        {
            return AddIncomingPhoneNumberAsync(options).Result;
        }

        /// <summary>
        /// Purchase/provision a local phone number
        /// </summary>
        /// <param name="options">Optional parameters to use when purchasing number</param>
        public Task<IncomingPhoneNumber> AddIncomingPhoneNumberAsync(PhoneNumberOptions options)
        {
            var request = new RestRequest(Method.POST) { Resource = "Accounts/{AccountSid}/IncomingPhoneNumbers.json" };

            if (options.PhoneNumber.HasValue())
            {
                request.AddParameter("PhoneNumber", options.PhoneNumber);
            }
            else
            {
                if (options.AreaCode.HasValue()) request.AddParameter("AreaCode", options.AreaCode);
            }

            Core.AddPhoneNumberOptionsToRequest(request, options);
            Core.AddSmsOptionsToRequest(request, options);

            return ExecuteAsync<IncomingPhoneNumber>(request);
        }

        /// <summary>
        /// Update the settings of an incoming phone number
        /// </summary>
        /// <param name="incomingPhoneNumberSid">The Sid of the phone number to update</param>
        /// <param name="options">Which settings to update. Only properties with values set will be updated.</param>
        public IncomingPhoneNumber UpdateIncomingPhoneNumber(string incomingPhoneNumberSid, PhoneNumberOptions options)
        {
            Require.Argument("IncomingPhoneNumberSid", incomingPhoneNumberSid);

            var request = new RestRequest(Method.POST) { Resource = "Accounts/{AccountSid}/IncomingPhoneNumbers/{IncomingPhoneNumberSid}.json" };
            request.AddParameter("IncomingPhoneNumberSid", incomingPhoneNumberSid, ParameterType.UrlSegment);

            Core.AddPhoneNumberOptionsToRequest(request, options);
            Core.AddSmsOptionsToRequest(request, options);

            return Execute<IncomingPhoneNumber>(request);
        }

        /// <summary>
        /// Remove (deprovision) a phone number from the current account
        /// </summary>
        /// <param name="incomingPhoneNumberSid">The Sid of the number to remove</param>
        public DeleteStatus DeleteIncomingPhoneNumber(string incomingPhoneNumberSid)
        {
            return DeleteIncomingPhoneNumberAsync(incomingPhoneNumberSid).Result;
        }

        /// <summary>
        /// Remove (deprovision) a phone number from the current account
        /// </summary>
        /// <param name="incomingPhoneNumberSid">The Sid of the number to remove</param>
        public async Task<DeleteStatus> DeleteIncomingPhoneNumberAsync(string incomingPhoneNumberSid)
        {
            Require.Argument("IncomingPhoneNumberSid", incomingPhoneNumberSid);

            var request = new RestRequest(Method.DELETE) { Resource = "Accounts/{AccountSid}/IncomingPhoneNumbers/{IncomingPhoneNumberSid}.json" };
            request.AddParameter("IncomingPhoneNumberSid", incomingPhoneNumberSid, ParameterType.UrlSegment);

            var response = await _client.ExecuteTaskAsync(request);

            return response.StatusCode == System.Net.HttpStatusCode.NoContent ? DeleteStatus.Success : DeleteStatus.Failed;
        }

        #endregion
    }
}
