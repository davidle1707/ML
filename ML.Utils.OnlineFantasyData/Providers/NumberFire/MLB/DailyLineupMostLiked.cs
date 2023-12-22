using System;
using System.Collections.Generic;

namespace ML.Utils.OnlineFantasyData.Providers.NumberFire.MLB
{
	[Serializable]
	public class DailyLineupMostLiked
	{
		public DailyLineupMostLiked()
		{
			lineup_players = new Dictionary<string, List<DailyLineupPlayer>>();

			players = new Dictionary<string, PlayerInfo>();
		}

		public Dictionary<string, List<DailyLineupPlayer>> lineup_players { get; set; }

		/// <summary>
		/// Key (player id) - Value (player info)
		/// </summary>
		public Dictionary<string, PlayerInfo> players { get; set; }

		public bool IsValid
		{
			get { return lineup_players != null && lineup_players.Count > 0 && players != null && players.Count > 0; }
		}
	}
}
