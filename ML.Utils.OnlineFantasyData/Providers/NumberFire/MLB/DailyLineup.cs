using System;
using System.Collections.Generic;

namespace ML.Utils.OnlineFantasyData.Providers.NumberFire.MLB
{
	[Serializable]
	public class DailyLineup
	{
		public DailyLineup()
		{
			best_value_players = new DailyBestPlayer();

			most_used_players = new DailyMostUsedPlayer();

			projections = new Dictionary<string, string>();

			//most_liked_lineup = new DailyLineupMostLiked();
		}

		public DailyBestPlayer best_value_players { get; set; }

		public DailyMostUsedPlayer most_used_players { get; set; }

		//public DailyLineupMostLiked most_liked_lineup { get; set; }

		public Dictionary<string, string> projections { get; set; }
		
		public bool IsValidBestPlayers
		{
			get { return best_value_players != null && best_value_players.IsValid; }
		}

		public bool IsValidMostUserdPlayers
		{
			get { return most_used_players != null && most_used_players.IsValid && projections != null && projections.Count > 0; }
		}

		//public bool IsValidMostLikedLineup
		//{
		//	get { return most_liked_lineup != null && most_liked_lineup.IsValid; }
		//}
	}
}
