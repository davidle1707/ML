using System;

namespace ML.Utils.OnlineFantasyData.Providers.NumberFire.Inc
{
	[Serializable]
	public class BaseDailyProjectionByJs : SiteReferInfo
	{
		public string season { get; set; }
		public string date { get; set; }

		public virtual string player_id { get; set; }
		public virtual string team_id { get; set; }
		public virtual string game_id { get; set; }

		public string opponent_id { get; set; }
	}
}
