using System;

namespace ML.Utils.IpLookup
{
	[Serializable]
	public class IpInfo
	{
        public string Ip { get; set; }

        public string Country { get; set; }

        public string CountryCode { get; set; }

		public string Region { get; set; }

        public string RegionCode { get; set; }

        public string City { get; set; }

		public bool IsValid()
		{
			return (!string.IsNullOrEmpty(Country) || !string.IsNullOrEmpty(CountryCode)) &&
				   (!string.IsNullOrEmpty(Region) || !string.IsNullOrEmpty(RegionCode));
		}

		public string ByProvider { get; internal set; }
	}
}
