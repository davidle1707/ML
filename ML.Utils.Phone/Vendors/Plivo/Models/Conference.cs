using System.Collections.Generic;

namespace ML.Utils.Phone.Vendors.Plivo.Models
{
	public class Conference
	{
		public string error { get; set; }
		public string api_id { get; set; }
		public string conference_member_count { get; set; }
		public string conference_name { get; set; }
		public string conference_run_time { get; set; }
		public List<ConferenceMember> members { get; set; }
	}
}