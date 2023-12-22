using System.Collections.Generic;

namespace ML.Utils.Phone.Vendors.Plivo.Models
{
	public class NumberList
	{
		public NumberMeta meta { get; set; }
		public string api_id { get; set; }
		public string error { get; set; }
		public List<Number> objects { get; set; }
	}
}