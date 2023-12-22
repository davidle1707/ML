using System.Collections.Generic;

namespace ML.Utils.Phone.Vendors.Plivo.Models
{
	public class OutgoingCarrierList
	{
		public OutgoingCarrierMeta meta { get; set; }
		public string api_id { get; set; }
		public string error { get; set; }
		public List<OutgoingCarrier> objects { get; set; }
	}
}