using System.Collections.Generic;

namespace ML.Utils.Phone.Vendors.Plivo.Models
{
	public class LiveConferenceList
	{
		public string error { get; set; }
		public string api_id { get; set; }
		public List<string> conferences { get; set; }
	}
}