using System.Collections.Generic;

namespace ML.Utils.Phone.Vendors.Plivo.Models
{
	public class GetMessagesResponse : BaseResponse
	{
		public MessageMeta meta { get; set; }
		
		public List<Message> objects { get; set; }
	}
}