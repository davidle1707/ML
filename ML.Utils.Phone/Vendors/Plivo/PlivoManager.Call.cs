using ML.Utils.Phone.Vendors.Plivo.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using dict = System.Collections.Generic.Dictionary<string, string>;

namespace ML.Utils.Phone.Vendors.Plivo
{
    public partial class PlivoManager
    {
		public IRestResponse<CDRList> GetCdrs()
		{
			return ExecuteRequest<CDRList>("GET", "/Call/", new dict());
		}

		public IRestResponse<CDRList> GetCdrs(dict parameters)
		{
			return ExecuteRequest<CDRList>("GET", "/Call/", parameters);
		}

		public IRestResponse<CDR> GetCdr(dict parameters)
		{
			var record_id = GetValueAndRemove(ref parameters, "record_id");
			return ExecuteRequest<CDR>("GET", $"/Call/{record_id}/", parameters);
		}

		public IRestResponse<LiveCallList> GetLiveCalls()
		{
			var parameters = new dict();
			parameters.Add("status", "live");
			return ExecuteRequest<LiveCallList>("GET", "/Call/", parameters);
		}

		public IRestResponse<LiveCall> GetLiveCall(dict parameters)
		{
			var call_uuid = GetValueAndRemove(ref parameters, "call_uuid");
			parameters.Add("status", "live");
			return ExecuteRequest<LiveCall>("GET", $"/Call/{call_uuid}/", parameters);
		}

		public IRestResponse<Call> MakeCall(dict parameters)
		{
			return ExecuteRequest<Call>("POST", "/Call/", parameters);
		}

		public IRestResponse<Call> MakeBulkCall(dict parameters, dict destNumberSipHeaders)
		{
			var destNumbers = "";
			var headerSIP = "";
			foreach (var kvp in destNumberSipHeaders)
			{
				destNumbers += kvp.Key + "<";
				headerSIP += kvp.Value + "<";
			}
			parameters.Add("to", destNumbers.Substring(0, destNumbers.Length - 1));
			parameters.Add("sip_headers", headerSIP.Substring(0, headerSIP.Length - 1));
			return ExecuteRequest<Call>("POST", "/Call/", parameters);
		}

		public IRestResponse<BaseResponse> HangupAllCalls()
		{
			return ExecuteRequest<BaseResponse>("DELETE", "/Call/", new dict());
		}

		public IRestResponse<BaseResponse> HangupCall(dict parameters)
		{
			var call_uuid = GetValueAndRemove(ref parameters, "call_uuid");
			return ExecuteRequest<BaseResponse>("DELETE", $"/Call/{call_uuid}/", parameters);
		}

		public IRestResponse<BaseResponse> TransferCall(dict parameters)
		{
			var call_uuid = GetValueAndRemove(ref parameters, "call_uuid");
			return ExecuteRequest<BaseResponse>("POST", $"/Call/{call_uuid}/", parameters);
		}

		public IRestResponse<Record> Record(dict parameters)
		{
			var call_uuid = GetValueAndRemove(ref parameters, "call_uuid");
			return ExecuteRequest<Record>("POST", $"/Call/{call_uuid}/Record/", parameters);
		}

		public IRestResponse<BaseResponse> StopRecord(dict parameters)
		{
			var call_uuid = GetValueAndRemove(ref parameters, "call_uuid");
			return ExecuteRequest<BaseResponse>("DELETE", $"/Call/{call_uuid}/Record/", parameters);
		}

		public IRestResponse<BaseResponse> Play(dict parameters)
		{
			var call_uuid = GetValueAndRemove(ref parameters, "call_uuid");
			return ExecuteRequest<BaseResponse>("POST", $"/Call/{call_uuid}/Play/", parameters);
		}

		public IRestResponse<BaseResponse> StopPlay(dict parameters)
		{
			var call_uuid = GetValueAndRemove(ref parameters, "call_uuid");
			return ExecuteRequest<BaseResponse>("DELETE", $"/Call/{call_uuid}/Play/", parameters);
		}

		public IRestResponse<BaseResponse> Speak(dict parameters)
		{
			var call_uuid = GetValueAndRemove(ref parameters, "call_uuid");
			return ExecuteRequest<BaseResponse>("POST", $"/Call/{call_uuid}/Speak/", parameters);
		}

		public IRestResponse<BaseResponse> StopSpeak(dict parameters)
		{
			var call_uuid = GetValueAndRemove(ref parameters, "call_uuid");
			return ExecuteRequest<BaseResponse>("DELETE", $"/Call/{call_uuid}/Speak/", parameters);
		}

		public IRestResponse<BaseResponse> SendDigits(dict parameters)
		{
			var call_uuid = GetValueAndRemove(ref parameters, "call_uuid");
			return ExecuteRequest<BaseResponse>("POST", $"/Call/{call_uuid}/DTMF/", parameters);
		}
    }
}
