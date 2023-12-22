using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML.Utils.Phone.Vendors
{
	public abstract class VendorSetting
	{
        public string ApiUrl { get; set; }

        public string ApiVersion { get; set; }

        public string AuthToken { get; set; }

        public string From { get; set; }

        public string ApplicationId { get; set; }

        public abstract VendorType Type { get; }

		public abstract bool IsValid();
	}
}
