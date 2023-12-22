using System.Collections.Generic;

namespace ML.Utils.Phone.Vendors.Plivo.Models
{
	public class BuyPhoneNumberResponse : BaseResponse
	{
		public List<PhoneNumberStatus> numbers { get; set; }

		public string status { get; set; }
	}
}