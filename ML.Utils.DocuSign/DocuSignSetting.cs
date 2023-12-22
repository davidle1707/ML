
namespace ML.Utils.DocuSign
{
	public class DocuSignSetting
	{
		public DocuSignSetting()
		{
			ThrowExceptionIfError = true;
		}

		public string IntegratorKey { get; set; }

		public string AccountId { get; set; }

		public string Email { get; set; }

		public string Password { get; set; }

		/// <summary>
		/// default value: true
		/// </summary>
		public bool ThrowExceptionIfError { get; set; }
        
		public bool IsValid()
		{
			return !string.IsNullOrEmpty(IntegratorKey) && !string.IsNullOrEmpty(AccountId) 
				&& !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password);
		}
	}
}
