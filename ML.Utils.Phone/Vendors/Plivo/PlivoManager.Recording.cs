using ML.Utils.Phone.Vendors.Plivo.Models;
using RestSharp;
using System;
using dict = System.Collections.Generic.Dictionary<string, string>;

namespace ML.Utils.Phone.Vendors.Plivo
{
    public partial class PlivoManager
    {
		public IRestResponse<RecordingList> GetRecordings()
		{
			return ExecuteRequest<RecordingList>("GET", "/Recording/", new dict());
		}

		public IRestResponse<Recording> GetRecording(dict parameters)
		{
			var recordingId = GetValueAndRemove(ref parameters, "recording_id");
			return ExecuteRequest<Recording>("GET", $"/Recording/{recordingId}/", parameters);
		}

		public IRestResponse<RecordingList> GetRecordingByCallUuid(dict parameters)
		{
			var callUUID = GetValueAndRemove(ref parameters, "call_uuid");
			return ExecuteRequest<RecordingList>("GET", "/Recording/?call_uuid=" + callUUID, new dict());
		}

		public IRestResponse<BaseResponse> DeleteRecording(dict parameters)
		{
			var recordingId = GetValueAndRemove(ref parameters, "recording_id");
			return ExecuteRequest<BaseResponse>("DELETE", $"/Recording/{recordingId}/", parameters);
		}
    }
}
