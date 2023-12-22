using System.Collections.Generic;

namespace ML.Utils.Phone.Vendors.Plivo.Models
{
	public class SendMessageResponse : BaseResponse
	{
		public List<string> message_uuid { get; set; }
	}
}