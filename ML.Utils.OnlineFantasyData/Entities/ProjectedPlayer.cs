using System;
using System.Collections.Generic;

namespace ML.Utils.OnlineFantasyData.Entities
{
	[Serializable]
	public class ProjectedPlayer : Player
	{
		public ProjectedPlayer()
			: base()
		{
			Ratios = new List<decimal>();
		}

		public ProjectedPlayer(string id, string fullName, string teamId)
			: base(id, fullName, teamId)
		{
			Ratios = new List<decimal>();
		}

		public string GameId { get; set; }

		public string OpponentId { get; set; }

		public string Opponent { get; set; }

		public bool IsBest { get; set; }

		internal decimal Ratio
		{
			get { return Ratios.Count > 0 ? Ratios[0] : 0; }
			set
			{
				if (Ratios.Count > 0)
				{
					Ratios[0] = value;
				}
				else
				{
					Ratios.Add(value);
				}
			}
		}

		internal List<decimal> Ratios { get; set; }
	}
}
