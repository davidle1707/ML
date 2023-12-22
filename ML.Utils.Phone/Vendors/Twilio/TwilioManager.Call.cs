using RestSharp;
using RestSharp.Extensions;
using RestSharp.Validation;

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

		/// <summary>
		/// Returns a paged list of phone calls made to and from the account.
		/// Sorted by DateUpdated with most-recent calls first.
		/// Makes a GET request to the Calls List resource.
		/// </summary>
		public CallResult ListCalls()
		{
			var request = new RestRequest();
			request.Resource = "Accounts/{AccountSid}/Calls.json";

			return Execute<CallResult>(request);
		}

		/// <summary>
		/// Returns a paged list of phone calls made to and from the account.
		/// Sorted by DateUpdated with most-recent calls first.
		/// Makes a GET request to the Calls List resource.
		/// </summary>
		/// <param name="options">List filter options. If an property is set the list will be filtered by that value.</param>
		public CallResult ListCalls(CallListRequest options)
		{
			var request = new RestRequest();
			request.Resource = "Accounts/{AccountSid}/Calls.json";

			AddCallListOptions(options, request);

			return Execute<CallResult>(request);
		}

		/// <summary>
		/// Returns the single Call resource identified by {CallSid}
		///  Makes a GET request to a Call Instance resource.
		/// </summary>
		/// <param name="callSid">The Sid of the Call resource to retrieve</param>
		public Call GetCall(string callSid)
		{
			var request = new RestRequest();
			request.Resource = "Accounts/{AccountSid}/Calls/{CallSid}.json";

			request.AddParameter("CallSid", callSid, ParameterType.UrlSegment);

			return Execute<Call>(request);
		}

		/// <summary>
		/// Initiates a new phone call. Makes a POST request to the Calls List resource.
		/// </summary>
		/// <param name="from">The phone number to use as the caller id. Format with a '+' and country code e.g., +16175551212 (E.164 format). Must be a Twilio number or a valid outgoing caller id for your account.</param>
		/// <param name="to">The number to call formatted with a '+' and country code e.g., +16175551212 (E.164 format). Twilio will also accept unformatted US numbers e.g., (415) 555-1212, 415-555-1212.</param>
		/// <param name="url">The fully qualified URL that should be consulted when the call connects. Just like when you set a URL for your inbound calls. URL should return TwiML.</param>
		public Call InitiateOutboundCall(string from, string to, string url)
		{
			return InitiateOutboundCall(from, to, url, string.Empty);
		}

		/// <summary>
		/// Initiates a new phone call. Makes a POST request to the Calls List resource.
		/// </summary>
		/// <param name="from">The phone number to use as the caller id. Format with a '+' and country code e.g., +16175551212 (E.164 format). Must be a Twilio number or a valid outgoing caller id for your account.</param>
		/// <param name="to">The number to call formatted with a '+' and country code e.g., +16175551212 (E.164 format). Twilio will also accept unformatted US numbers e.g., (415) 555-1212, 415-555-1212.</param>
		/// <param name="url">The fully qualified URL that should be consulted when the call connects. Just like when you set a URL for your inbound calls. URL should return TwiML.</param>
		/// <param name="statusCallback">A URL that Twilio will request when the call ends to notify your app.</param>
		public Call InitiateOutboundCall(string from, string to, string url, string statusCallback)
		{
			return InitiateOutboundCall(new CallOptions
			{
				From = from,
				To = to,
				Url = url,
				StatusCallback = statusCallback
			});
		}

		/// <summary>
		/// Initiates a new phone call. Makes a POST request to the Calls List resource.
		/// </summary>
		/// <param name="options">Call settings. Only properties with values set will be used.</param>
		public Call InitiateOutboundCall(CallOptions options)
		{
			Require.Argument("From", options.From);
			Require.Argument("To", options.To);

			var request = new RestRequest(Method.POST);
			request.Resource = "Accounts/{AccountSid}/Calls.json";

			AddCallOptions(options, request);

			return Execute<Call>(request);
		}

		/// <summary>
		/// Hangs up a call in progress. Makes a POST request to a Call Instance resource.
		/// </summary>
		/// <param name="callSid">The Sid of the call to hang up.</param>
		/// <param name="style">'Canceled' will attempt to hangup calls that are queued or ringing but not affect calls already in progress. 'Completed' will attempt to hang up a call even if it's already in progress.</param>
		public Call HangupCall(string callSid, HangupStyle style)
		{
			Require.Argument("CallSid", callSid);

			var request = new RestRequest(Method.POST);
			request.Resource = "Accounts/{AccountSid}/Calls/{CallSid}.json";

			request.AddUrlSegment("CallSid", callSid);
			request.AddParameter("Status", style.ToString().ToLower());

			return Execute<Call>(request);
		}

		/// <summary>
		/// Redirect a call in progress to a new TwiML URL.  Makes a POST request to a Call Instance resource.
		/// </summary>
		/// <param name="callSid">The Sid of the call to redirect</param>
		/// <param name="redirectUrl">The URL to redirect the call to.</param>
		/// <param name="redirectMethod">The HTTP method to use when requesting the redirectUrl</param>
		public Call RedirectCall(string callSid, string redirectUrl, string redirectMethod)
		{
			Require.Argument("CallSid", callSid);
			Require.Argument("Url", redirectUrl);

			var request = new RestRequest(Method.POST);
			request.Resource = "Accounts/{AccountSid}/Calls/{CallSid}.json";

			request.AddParameter("CallSid", callSid, ParameterType.UrlSegment);
			request.AddParameter("Url", redirectUrl);
			if (redirectMethod.HasValue()) request.AddParameter("Method", redirectMethod);

			return Execute<Call>(request);
		}

		private static void AddCallOptions(CallOptions options, RestRequest request)
		{
			request.AddParameter("From", options.From);
			request.AddParameter("To", options.To);

			if (options.ApplicationSid.HasValue())
			{
				request.AddParameter("ApplicationSid", options.ApplicationSid);
			}
			else
			{
				request.AddParameter("Url", options.Url);
			}

			if (options.StatusCallback.HasValue()) request.AddParameter("StatusCallback", options.StatusCallback);
			if (options.StatusCallbackMethod.HasValue()) request.AddParameter("StatusCallbackMethod", options.StatusCallbackMethod);
			if (options.FallbackUrl.HasValue()) request.AddParameter("FallbackUrl", options.FallbackUrl);
			if (options.FallbackMethod.HasValue()) request.AddParameter("FallbackMethod", options.FallbackMethod);
			if (options.Method.HasValue()) request.AddParameter("Method", options.Method);
			if (options.SendDigits.HasValue()) request.AddParameter("SendDigits", options.SendDigits);
			if (options.IfMachine.HasValue()) request.AddParameter("IfMachine", options.IfMachine);
			if (options.Timeout.HasValue) request.AddParameter("Timeout", options.Timeout.Value);
			if (options.Record) request.AddParameter("Record", "true");
		}

		private void AddCallListOptions(CallListRequest options, RestRequest request)
		{
			if (options.From.HasValue()) request.AddParameter("From", options.From);
			if (options.To.HasValue()) request.AddParameter("To", options.To);
			if (options.Status.HasValue()) request.AddParameter("Status", options.Status);
			if (options.StartTime.HasValue) request.AddParameter("StartTime", options.StartTime.Value.ToString("yyyy-MM-dd"));
			if (options.EndTime.HasValue) request.AddParameter("EndTime", options.EndTime.Value.ToString("yyyy-MM-dd"));

			var startTimeParameterName = GetParameterNameWithEquality(options.StartTimeComparison, "StartTime");
			var endTimeParameterName = GetParameterNameWithEquality(options.EndTimeComparison, "EndTime");

			if (options.StartTime.HasValue) request.AddParameter(startTimeParameterName, options.StartTime.Value.ToString("yyyy-MM-dd"));
			if (options.EndTime.HasValue) request.AddParameter(endTimeParameterName, options.EndTime.Value.ToString("yyyy-MM-dd"));

			if (options.Count.HasValue) request.AddParameter("PageSize", options.Count.Value);
			if (options.PageNumber.HasValue) request.AddParameter("Page", options.PageNumber.Value);

			if (options.ParentCallSid.HasValue()) request.AddParameter("ParentCallSid", options.ParentCallSid);
		}

		private string GetParameterNameWithEquality(ComparisonType? comparisonType, string parameterName)
		{
			if (comparisonType.HasValue)
			{
				switch (comparisonType)
				{
					case ComparisonType.GreaterThanOrEqualTo:
						parameterName += ">";
						break;
					case ComparisonType.LessThanOrEqualTo:
						parameterName += "<";
						break;
				}
			}
			return parameterName;
		}

	}
}
