
namespace ML.Utils.Phone
{
	public class SendSmsResponse
	{
		public SendSmsResponse()
		{
			Status = SendSmsStatus.Success;
			Description = "";
		}

		public SendSmsStatus Status { get; set; }

		public string Description { get; set; }

	    public string FromPhone { get; set; }

        public VendorType? FromVendor { get; set; }

		public string ExternalSmsId { get; set; }

		public decimal ExternalSmsPrice { get; set; }

        public string ExternalSmsJson { get; set; }
    }

	public enum SendSmsStatus : short
	{
		Success = 1,

		Unsuccess,

		Error

		//DONT MOVE POSITION ITEM
	}
}
