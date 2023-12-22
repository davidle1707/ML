using System.Collections.Generic;

namespace ML.Utils.Phone.Vendors.Plivo.Models
{
	public class RecordingList
	{
		public RecordingMeta meta { get; set; }
		public string error { get; set; }
		public string api_id { get; set; }
		public List<Recording> objects { get; set; }
	}
}