using System.Collections.Generic;

namespace ML.Utils.Phone.Vendors.Plivo.Models
{
	public class CDRList
	{
		public CDRMeta meta { get; set; }
		public string error { get; set; }
		public string api_id { get; set; }
		public List<CDR> objects { get; set; }
	}
}