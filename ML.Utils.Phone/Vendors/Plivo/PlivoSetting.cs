using ML.Common;

namespace ML.Utils.Phone.Vendors.Plivo
{
    /// <summary>
    /// Url (https://api.plivo.com) Version (v1)
    /// </summary>
	public class PlivoSetting : VendorSetting
	{
		public PlivoSetting(bool fromAppSetting = false)
		{
			ApiUrl = "https://api.plivo.com";

			ApiVersion = "v1";

			if (fromAppSetting)
			{
				ApiUrl = std.AppSettings["PLIVO_API_URL"].ToStr();
				ApiVersion = std.AppSettings["PLIVO_API_VERSION"].ToStr();
				AuthId = std.AppSettings["PLIVO_AUTH_ID"].ToStr();
				AuthToken = std.AppSettings["PLIVO_AUTH_TOKEN"].ToStr();
				From = std.AppSettings["PLIVO_FROM"].ToStr();
			}
		}

		public string AuthId { get; set; }

        public override bool IsValid()
		{
			return !string.IsNullOrWhiteSpace(ApiUrl) && !string.IsNullOrWhiteSpace(ApiVersion) 
			       && !string.IsNullOrWhiteSpace(AuthId) && !string.IsNullOrWhiteSpace(AuthToken);
		}

        public override VendorType Type => VendorType.Plivo;
	}
}
