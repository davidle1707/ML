using System;
using System.Collections.Generic;
using System.Linq;

namespace ML.Utils.OnlineFantasyData.Providers.NumberFire.MLB
{
	[Serializable]
	public class DailyLineupResponse
	{
		public DailyLineupResponse()
		{
			site_lineups = new Dictionary<string, DailyLineup>();
		}

		/// <summary>
		/// Key (site name) - Value (lineup info)
		/// </summary>
		public Dictionary<string, DailyLineup> site_lineups { get; set; }

		public bool IsValid
		{
			get { return site_lineups != null && site_lineups.Count > 0 && site_lineups.Values.Any(v => v != null); }
		}
	}
}
