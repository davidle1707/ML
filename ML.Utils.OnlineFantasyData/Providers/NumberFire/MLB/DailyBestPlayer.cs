using System;
using System.Collections.Generic;

namespace ML.Utils.OnlineFantasyData.Providers.NumberFire.MLB
{
	[Serializable]
	public class DailyBestPlayer
	{
		public DailyBestPlayer()
		{
			projections = new List<DailyBestPlayerProjection>();

			players = new Dictionary<string, PlayerInfo>();
		}

		public List<DailyBestPlayerProjection> projections { get; set; }

		/// <summary>
		/// Key (player id) - Value (player info)
		/// </summary>
		public Dictionary<string, PlayerInfo> players { get; set; }

		public bool IsValid
		{
			get { return projections != null && projections.Count > 0 && players != null && players.Count > 0; }
		}
	}
}
