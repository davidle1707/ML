
namespace ML.Utils.Phone.Vendors.Plivo.Models
{
	public class BuyPhoneNumberRequest : BaseRequest
	{
		/// <summary>
		/// Mandatory
		/// </summary>
		public string number { get; set; }

		/// <summary>
		/// The ID of the application that you want assigned to the Number. If not specified, then it is assigned to the default application of the Account.
		/// </summary>
		public string app_id { get; set; }
	}
}
