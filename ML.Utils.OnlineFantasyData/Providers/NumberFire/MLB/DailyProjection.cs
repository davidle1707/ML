using System;
using ML.Utils.OnlineFantasyData.Providers.NumberFire.Inc;

namespace ML.Utils.OnlineFantasyData.Providers.NumberFire.MLB
{
	[Serializable]
	public class DailyProjection : BaseDailyProjectionByJs
	{
		public string mlb_player_id { get; set; }
		public string mlb_game_id { get; set; }
		public string mlb_team_id { get; set; }

		public override string player_id { get { return mlb_player_id; } }
		public override string team_id { get { return mlb_team_id; } }
		public override string game_id { get { return mlb_game_id; } }

		public string pa { get; set; }
		public string ab { get; set; }
		public string r { get; set; }
		public string h { get; set; }
		public string h1b { get; set; }
		public string h2b { get; set; }
		public string h3b { get; set; }
		public string hr { get; set; }
		public string rbi { get; set; }
		public string sb { get; set; }
		public string cs { get; set; }
		public string bb { get; set; }
		public string so { get; set; }
		public string ba { get; set; }
		public string obp { get; set; }
		public string slg { get; set; }
		public string ops { get; set; }
		public string hbp { get; set; }
		public string ibb { get; set; }
		public string sh { get; set; }
		public string sf { get; set; }
	}
}
