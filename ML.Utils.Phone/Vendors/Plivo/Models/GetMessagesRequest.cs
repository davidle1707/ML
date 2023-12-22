using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML.Utils.Phone.Vendors.Plivo.Models
{
	public class GetMessagesRequest
	{
		public GetMessagesRequest()
		{
			limit = 20;

			offset = 0;

		}

		/// <summary>
		/// Used to display the number of results per page. The maximum number of results that can be fetched is 20.
		/// </summary>
		public int? limit { get; set; }

		/// <summary>
		/// Denotes the number of value items by which the results should be offset. Eg:- If the result contains a 1000 values and limit is set to 10 and offset is set to 705, then values 706 through 715 are displayed in the results. This parameter is also used for pagination of the results.
		/// </summary>
		public int? offset { get; set; }
	}
}
