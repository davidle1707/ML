using System.Collections.Generic;

namespace ML.Utils.Phone.Vendors.Plivo.Models
{
	public class SubAccountList
	{
		public SubAccountMeta meta { get; set; }
		public string error { get; set; }
		public string api_id { get; set; }
		public List<SubAccount> objects { get; set; }
	}
}