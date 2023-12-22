using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ML.Common;

namespace ML.Utils.Phone.Vendors.Tropo
{
    /// <summary>
    /// Url (http://api.tropo.com) Version (1.0)
    /// </summary>
	public class TropoSetting : VendorSetting
	{
		public TropoSetting(bool fromAppSetting = false)
		{
			ApiUrl = "http://api.tropo.com";

			ApiVersion = "1.0";

			if (fromAppSetting)
			{
				ApiUrl = std.AppSettings["TROPO_API_URL"].ToStr();
				ApiVersion = std.AppSettings["TROPO_API_VERSION"].ToStr();
				VoiceToken = std.AppSettings["TROPO_VOICE_TOKEN"].ToStr();
				MessageToken = std.AppSettings["TROPO_MESSAGE_TOKEN"].ToStr();
				From = std.AppSettings["TROPO_FROM"].ToStr();
			}
		}

		public string MessageToken { get; set; }

		public string VoiceToken { get; set; }

		public override bool IsValid()
		{
			return !string.IsNullOrWhiteSpace(ApiUrl) && !string.IsNullOrWhiteSpace(ApiVersion)
				&& (!string.IsNullOrWhiteSpace(VoiceToken) || !string.IsNullOrWhiteSpace(MessageToken));
		}

        public override VendorType Type => VendorType.Tropo;
	}
}
