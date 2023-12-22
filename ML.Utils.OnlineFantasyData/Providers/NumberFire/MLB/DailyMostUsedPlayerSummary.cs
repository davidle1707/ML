using System;

namespace ML.Utils.OnlineFantasyData.Providers.NumberFire.MLB
{
	[Serializable]
	public class DailyMostUsedPlayerSummary
	{
		public string site { get; set; }
		public string date { get; set; }
		public string player_id { get; set; }
		public string position { get; set; }
		public string salary { get; set; }
		public string lineup_count { get; set; }
	}
}
