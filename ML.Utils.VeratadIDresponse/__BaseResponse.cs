using System;

namespace ML.Utils.VeratadIDresponse
{
	[Serializable]
	public class BaseResponse
	{
		public BaseResponse()
		{
			TransactionId = Guid.NewGuid().ToString();
			TransactionDate = DateTime.Now;
			State = ResultState.Error;
		}

		public string TransactionId { get; internal set; }

		public DateTime TransactionDate { get; set; }

		public ResultState State { get; set; }

		public string Description { get; set; }
	}
}
