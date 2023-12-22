using System.Collections.Generic;

namespace ML.Utils.Phone.Vendors.Plivo.Models
{
	public class OutgoingCarrierRoutingList
	{
		public ResourceListMeta meta { get; set; }
		public string api_id { get; set; }
		public string error { get; set; }
		public List<OutgoingCarrierRouting> objects { get; set; }
	}
}