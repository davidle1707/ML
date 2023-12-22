using System;

namespace ML.Utils.OnlineFantasyData.Providers.NumberFire.MLB
{
	[Serializable]
	public class DailyBestPlayerProjection
	{
		public string mlb_player_id { get; set; }
		public string mlb_game_id { get; set; }
		public string date { get; set; }
		public string season { get; set; }
		public string mlb_team_id { get; set; }
		public string opponent_id { get; set; }
		public string w { get; set; }
		public string l { get; set; }
		public string era { get; set; }
		public string gs { get; set; }
		public string cg { get; set; }
		public string sho { get; set; }
		public string qs { get; set; }
		public string sv { get; set; }
		public string hd { get; set; }
		public string bs { get; set; }
		public string ip { get; set; }
		public string h { get; set; }
		public string r { get; set; }
		public string er { get; set; }
		public string hr { get; set; }
		public string bb { get; set; }
		public string ibb { get; set; }
		public string so { get; set; }
		public string hbp { get; set; }
		public string bf { get; set; }
		public string whip { get; set; }
		public string fp { get; set; }
		public string salary { get; set; }
		public string value { get; set; }
	}
}
