using System;

namespace ML.Utils.OnlineFantasyData.Providers.NumberFire.MLB
{
	[Serializable]
	public class DailyLineupPlayer
	{
		public string lineup_id { get; set; }
		public string player_id { get; set; }
		public string position { get; set; }
		public string salary { get; set; }
	}
}
