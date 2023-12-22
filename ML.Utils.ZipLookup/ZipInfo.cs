using System;

namespace ML.Utils.ZipLookup
{
	[Serializable]
	public class ZipInfo
	{
		public string City { get; set; }

		public string State { get; set; }

		public string Zip { get; set; }

		public string Country { get; set; }

		public string FormatAddress
		{
			get { return string.Format("{0}, {1} {2}, {3}", City, State, Zip, Country); }
		}

		public string ByProvider { get; internal set; }
	}
}
