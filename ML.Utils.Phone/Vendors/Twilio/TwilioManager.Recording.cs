using RestSharp;
using RestSharp.Extensions;
using System;

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
		/// Returns a list of Recordings, each representing a recording generated during the course of a phone call. 
		/// The list includes paging information.
		/// Makes a GET request to the Recordings List resource.
		/// </summary>
		public RecordingResult ListRecordings()
		{
			return ListRecordings(null, null, null, null);
		}

		/// <summary>
		/// Returns a filtered list of Recordings, each representing a recording generated during the course of a phone call. 
		/// The list includes paging information.
		/// Makes a GET request to the Recordings List resource.
		/// </summary>
		/// <param name="callSid">(Optional) The CallSid to retrieve recordings for</param>
		/// <param name="dateCreated">(Optional) The date the recording was created (GMT)</param>
		/// <param name="pageNumber">The page to start retrieving results from</param>
		/// <param name="count">How many results to retrieve</param>
		public RecordingResult ListRecordings(string callSid, DateTime? dateCreated, int? pageNumber, int? count)
		{
			var request = new RestRequest();
			request.Resource = "Accounts/{AccountSid}/Recordings.json";

			if (callSid.HasValue()) request.AddParameter("CallSid", callSid);
			if (dateCreated.HasValue) request.AddParameter("DateCreated", dateCreated.Value.ToString("yyyy-MM-dd"));
			if (pageNumber.HasValue) request.AddParameter("Page", pageNumber.Value);
			if (count.HasValue) request.AddParameter("PageSize", count.Value);

			return Execute<RecordingResult>(request);
		}

		/// <summary>
		/// Retrieve the details for the specified recording instance.
		/// Makes a GET request to a Recording Instance resource.
		/// </summary>
		/// <param name="recordingSid">The Sid of the recording to retrieve</param>
		public Recording GetRecording(string recordingSid)
		{
			var request = new RestRequest();
			request.Resource = "Accounts/{AccountSid}/Recordings/{RecordingSid}.json";

			request.AddParameter("RecordingSid", recordingSid, ParameterType.UrlSegment);

			return Execute<Recording>(request);
		}

		/// <summary>
		/// Delete the specified recording instance. Makes a DELETE request to a Recording Instance resource.
		/// </summary>
		/// <param name="recordingSid">The Sid of the recording to delete</param>
		public DeleteStatus DeleteRecording(string recordingSid)
		{
			var request = new RestRequest(Method.DELETE);
			request.Resource = "Accounts/{AccountSid}/Recordings/{RecordingSid}.json";

			request.AddParameter("RecordingSid", recordingSid, ParameterType.UrlSegment);

			var response = _client.Execute(request);
			return response.StatusCode == System.Net.HttpStatusCode.NoContent ? DeleteStatus.Success : DeleteStatus.Failed;
		}

		/// <summary>
		/// Retrieves the transcription text for the specified recording, if it was transcribed. 
		/// Makes a GET request to a Recording Instance resource.
		/// </summary>
		/// <param name="recordingSid">The Sid of the recording to retreive the transcription for</param>
		public string GetRecordingText(string recordingSid)
		{
			var request = new RestRequest();
			request.Resource = "Accounts/{AccountSid}/Recordings/{RecordingSid}.txt";
			request.AddParameter("RecordingSid", recordingSid, ParameterType.UrlSegment);

			var response = _client.Execute(request);
			return response.Content;
		}

	}
}
