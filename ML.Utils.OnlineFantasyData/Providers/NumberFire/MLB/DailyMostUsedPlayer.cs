using System;
using System.Collections.Generic;

namespace ML.Utils.OnlineFantasyData.Providers.NumberFire.MLB
{
	[Serializable]
	public class DailyMostUsedPlayer
	{
		public DailyMostUsedPlayer()
		{
			summaries = new List<DailyMostUsedPlayerSummary>();

			players = new Dictionary<string, PlayerInfo>();
		}

		public List<DailyMostUsedPlayerSummary> summaries { get; set; }

		/// <summary>
		/// Key (player id) - Value (player info)
		/// </summary>
		public Dictionary<string, PlayerInfo> players { get; set; }

		public bool IsValid { get { return summaries != null && summaries.Count > 0 && players != null && players.Count > 0; } }
	}
}
