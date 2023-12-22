
namespace ML.Utils.VeratadIDresponse
{
	public class VeratadIDresponseSetting
	{
		public VeratadIDresponseSetting()
		{
			ApiUrl = "https://production.idresponse.com/process/5/gateway";

			ThrowExceptionIfError = true;
		}

		/// <summary>
		/// default: https://production.idresponse.com/process/5/gateway
		/// </summary>
		public string ApiUrl { get; set; }

		/// <summary>
		/// default value: true
		/// </summary>
		public bool ThrowExceptionIfError { get; set; }

		public string UserName { get; set; }

		public string Password { get; set; }

		/// <summary>
		/// AgeMatch5.0; IDMatch5.0; IDMatchPLUS5.0_en; IDMatchPLUS5.0_es; IDMatch5.0.COMPLY; IDMatchPLUSCOMPLY5.0_en; IDMatchPLUSCOMPLY5.0_es
		/// </summary>
		internal string ServiceName { get; set; }

		public bool IsValid()
		{
			return !string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(ServiceName);
		}
	}
}
