using System.Collections.Generic;

namespace ML.Utils.Phone.Vendors.Plivo.Models
{
	public class SearchPhoneNumberResponse : BaseResponse
	{
		public PhoneNumberMeta meta { get; set; }
		
		public List<PhoneNumber> objects { get; set; }
	}
}