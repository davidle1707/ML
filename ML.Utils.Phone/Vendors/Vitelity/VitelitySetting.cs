
using ML.Common;

namespace ML.Utils.Phone.Vendors.Vitelity
{
    /// <summary>
    /// Url (http://api.vitelity.net/fax.php)
    /// </summary>
    public class VitelitySetting : VendorSetting
    {
		public VitelitySetting(bool fromAppSetting = false)
        {
            ApiUrl = "http://api.vitelity.net/fax.php";

			if (fromAppSetting)
			{
				ApiUrl = std.AppSettings["VITELITY_API_URL"].ToStr();
				Login = std.AppSettings["VITELITY_LOGIN"].ToStr();
				Password = std.AppSettings["VITELITY_PASSWORD"].ToStr();
			}
        }
        public string Login { get; set; }

        public string Password { get; set; }

		public override bool IsValid()
		{
			return !string.IsNullOrWhiteSpace(ApiUrl) && !string.IsNullOrWhiteSpace(Login)
			       && !string.IsNullOrWhiteSpace(Password);
		}

        public override VendorType Type => VendorType.Vitelity;
    }
}
