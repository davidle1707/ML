using System.Collections.Generic;

namespace ML.Utils.Phone.Vendors.Plivo.Models
{
	public class NumberResponse
	{
		public List<NumberStatus> numbers { get; set; }
		public string status { get; set; }
	}
}