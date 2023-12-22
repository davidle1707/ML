using System;
using ML.Utils.OnlineFantasyData.Providers.NumberFire.Inc;

namespace ML.Utils.OnlineFantasyData.Providers.NumberFire.NBA
{
	[Serializable]
	public class DailyProjection : BaseDailyProjectionByJs
	{
		public string nba_player_id { get; set; }
		public string nba_game_id { get; set; }
		public string nba_team_id { get; set; }

		public override string player_id { get { return nba_player_id; } }
		public override string team_id { get { return nba_team_id; } }
		public override string game_id { get { return nba_game_id; } }
	}
}
