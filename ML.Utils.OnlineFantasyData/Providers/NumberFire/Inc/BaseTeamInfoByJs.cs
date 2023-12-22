using System;

namespace ML.Utils.OnlineFantasyData.Providers.NumberFire.Inc
{
	[Serializable]
	public class BaseTeamInfoByJs
	{
		public string id { get; set; }
		public string name { get; set; }
		public string city { get; set; }
		public string @short { get; set; }
		public string abbrev { get; set; }
		public string slug { get; set; }
		public string espn_id { get; set; }
		public string league_id { get; set; }
		public string is_active { get; set; }
		public string conference { get; set; }
		public string division { get; set; }
	}
}
