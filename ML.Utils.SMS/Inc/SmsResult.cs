
namespace ML.Utils.SMS
{
	public class SmsResult
	{
		public SmsResult()
		{
			State = SmsState.Success;
			Description = "";
		}

		public SmsState State { get; set; }

		public string Description { get; set; }

		public string MessageId { get; set; }

		public decimal Price { get; set; }
	}
}
