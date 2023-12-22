using System.Collections.Generic;

namespace ML.Utils.Phone.Vendors.Plivo.Models
{
	public class IncomingCarrierList
	{
		public IncomingCarrierMeta meta { get; set; }
		public string api_id { get; set; }
		public string error { get; set; }
		public List<IncomingCarrier> objects { get; set; }
	}
}