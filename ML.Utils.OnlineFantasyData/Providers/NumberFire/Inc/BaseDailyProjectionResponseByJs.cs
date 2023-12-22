using System;
using System.Collections.Generic;
using System.Linq;

namespace ML.Utils.OnlineFantasyData.Providers.NumberFire.Inc
{
	[Serializable]
	public class BaseDailyProjectionResponseByJs<T> : BaseDailyProjectionResponse
		where T : BaseDailyProjectionByJs
	{
		protected BaseDailyProjectionResponseByJs()
		{
			teams = new Dictionary<string, BaseTeamInfoByJs>();

			positions = new Dictionary<string, DailyProjectionPositionByJs<T>>();
		}

		/// <summary>
		/// Key (team id) - Value (team info)
		/// </summary>
		public Dictionary<string, BaseTeamInfoByJs> teams { get; set; }

		/// <summary>
		/// Key (position) - Value (position info)
		/// </summary>
		public Dictionary<string, DailyProjectionPositionByJs<T>> positions { get; set; }

		public bool IsValid
		{
			get { return teams != null && teams.Count > 0 && positions != null && positions.Count > 0 && positions.Values.All(p => p.IsValid); }
		}
	}

	[Serializable]
	public class DailyProjectionPositionByJs<T> where T : BaseDailyProjectionByJs
	{
		public DailyProjectionPositionByJs()
		{
			projections = new List<T>();

			players = new Dictionary<string, BasePlayerInfoByJs>();
		}

		public List<T> projections { get; set; }

		/// <summary>
		/// Key (player id) - Value (player info)
		/// </summary>
		public Dictionary<string, BasePlayerInfoByJs> players { get; set; }

		public bool IsValid
		{
			get { return projections != null && projections.Count > 0 && players != null && players.Count > 0; }
		}
	}
}
