using ML.Utils.Phone.Vendors.Plivo.Models;
using RestSharp;
using System;
using dict = System.Collections.Generic.Dictionary<string, string>;

namespace ML.Utils.Phone.Vendors.Plivo
{
    public partial class PlivoManager
    {
		public IRestResponse<LiveConferenceList> GetLiveConferences()
		{
			return ExecuteRequest<LiveConferenceList>("GET", "/Conference/", new dict());
		}

		public IRestResponse<BaseResponse> HangupAllConferences()
		{
			return ExecuteRequest<BaseResponse>("DELETE", "/Conference/", new dict());
		}

		public IRestResponse<Conference> GetLiveConference(dict parameters)
		{
			var conference_name = GetValueAndRemove(ref parameters, "conference_name");
			return ExecuteRequest<Conference>("GET", $"/Conference/{conference_name}/", parameters);
		}

		public IRestResponse<BaseResponse> HangupConference(dict parameters)
		{
			var conference_name = GetValueAndRemove(ref parameters, "conference_name");
			return ExecuteRequest<BaseResponse>("DELETE", $"/Conference/{conference_name}/", parameters);
		}

		public IRestResponse<BaseResponse> HangupMember(dict parameters)
		{
			var conference_name = GetValueAndRemove(ref parameters, "conference_name");
			var member_id = GetValueAndRemove(ref parameters, "member_id");
			return ExecuteRequest<BaseResponse>("DELETE", $"/Conference/{conference_name}/Member/{member_id}/", parameters);
		}

		public IRestResponse<BaseResponse> PlayMember(dict parameters)
		{
			var conference_name = GetValueAndRemove(ref parameters, "conference_name");
			var member_id = GetValueAndRemove(ref parameters, "member_id");
			return ExecuteRequest<BaseResponse>("POST", $"/Conference/{conference_name}/Member/{member_id}/Play/", parameters);
		}

		public IRestResponse<BaseResponse> StopPlayMember(dict parameters)
		{
			var conference_name = GetValueAndRemove(ref parameters, "conference_name");
			var member_id = GetValueAndRemove(ref parameters, "member_id");
			return ExecuteRequest<BaseResponse>("DELETE", $"/Conference/{conference_name}/Member/{member_id}/Play/", parameters);
		}

		public IRestResponse<BaseResponse> SpeakMember(dict parameters)
		{
			var conference_name = GetValueAndRemove(ref parameters, "conference_name");
			var member_id = GetValueAndRemove(ref parameters, "member_id");
			return ExecuteRequest<BaseResponse>("POST", $"/Conference/{conference_name}/Member/{member_id}/Speak/", parameters);
		}

		public IRestResponse<BaseResponse> DeafMember(dict parameters)
		{
			var conference_name = GetValueAndRemove(ref parameters, "conference_name");
			var member_id = GetValueAndRemove(ref parameters, "member_id");
			return ExecuteRequest<BaseResponse>("POST", $"/Conference/{conference_name}/Member/{member_id}/Deaf/", parameters);
		}

		public IRestResponse<BaseResponse> UndeafMember(dict parameters)
		{
			var conference_name = GetValueAndRemove(ref parameters, "conference_name");
			var member_id = GetValueAndRemove(ref parameters, "member_id");
			return ExecuteRequest<BaseResponse>("DELETE", $"/Conference/{conference_name}/Member/{member_id}/Deaf/", parameters);
		}

		public IRestResponse<BaseResponse> MuteMember(dict parameters)
		{
			var conference_name = GetValueAndRemove(ref parameters, "conference_name");
			var member_id = GetValueAndRemove(ref parameters, "member_id");
			return ExecuteRequest<BaseResponse>("POST", $"/Conference/{conference_name}/Member/{member_id}/Mute/", parameters);
		}

		public IRestResponse<BaseResponse> UnmuteMember(dict parameters)
		{
			var conference_name = GetValueAndRemove(ref parameters, "conference_name");
			var member_id = GetValueAndRemove(ref parameters, "member_id");
			return ExecuteRequest<BaseResponse>("DELETE", $"/Conference/{conference_name}/Member/{member_id}/Mute/", parameters);
		}

		public IRestResponse<BaseResponse> KickMember(dict parameters)
		{
			var conference_name = GetValueAndRemove(ref parameters, "conference_name");
			var member_id = GetValueAndRemove(ref parameters, "member_id");
			return ExecuteRequest<BaseResponse>("POST", $"/Conference/{conference_name}/Member/{member_id}/Kick/", parameters);
		}

		public IRestResponse<Record> RecordConference(dict parameters)
		{
			var conference_name = GetValueAndRemove(ref parameters, "conference_name");
			return ExecuteRequest<Record>("POST", $"/Conference/{conference_name}/Record/", parameters);
		}

		public IRestResponse<BaseResponse> StopRecordConference(dict parameters)
		{
			var conference_name = GetValueAndRemove(ref parameters, "conference_name");
			return ExecuteRequest<BaseResponse>("DELETE", $"/Conference/{conference_name}/Record/", parameters);
		}

    }
}
