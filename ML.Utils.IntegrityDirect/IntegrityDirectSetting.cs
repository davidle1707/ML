using System;

namespace ML.Utils.IntegrityDirect
{
	[Serializable]
	public class IntegrityDirectSetting
	{
		public IntegrityDirectSetting()
		{
			ApiUrl = "https://www.integrity-direct.com/online/authentication_url.asp";

			ThrowExceptionIfError = true;
		}

		/// <summary>
		/// default: https://www.integrity-direct.com/online/authentication_url.asp
		/// </summary>
		public string ApiUrl { get; set; }

		public string SiteId { get; set; }

		/// <summary>
		/// default value: true
		/// </summary>
		public bool ThrowExceptionIfError { get; set; }

		public bool IsValid()
		{
			return !string.IsNullOrEmpty(ApiUrl) && !string.IsNullOrEmpty(SiteId);
		}
	}
}
