
namespace ML.Utils.Phone.Vendors.Plivo.Models
{
	public class SendMessageRequest : BaseRequest
	{
		/// <summary>
		/// Mandatory. The phone number to be used as the caller id (with the country code). For e.g. a USA number, `15671234567`. If you're sending messages to U.S. and Canada, you need to use the numbers you purchased with Plivo
		/// </summary>
		public string src { get; set; }

		/// <summary>
		/// Mandatory. The number to which the message needs to be sent. Regular phone numbers must be prefixed with the country code but without the + sign. For e.g, a USA phone number would be, `15677654321`, with '1' denoting the country code. Multiple numbers can be added by using a delimiter. For e.g. <![CDATA[15677654321<12077657621<12047657621]]>.
		/// </summary>
		public string dst { get; set; }

		/// <summary>
		/// Mandatory. The text to send encoded in Unicode UTF-8. The API accepts up to 1000 bytes of UTF-8 encoded text in a single API request. The text will be automatically split into multiple parts and sent if it will not fit into a single SMS. See notes below for more details.
		/// </summary>
		public string text { get; set; }
	}
}
