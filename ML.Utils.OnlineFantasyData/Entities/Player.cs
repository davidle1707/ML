using System;
using System.Collections.Generic;

namespace ML.Utils.OnlineFantasyData.Entities
{
	[Serializable]
	public class Player
	{
		public Player()
		{
			Positions = new List<string>();
			Fpps = new List<decimal>();
			Salaries = new List<decimal>();
		}

		public Player(string id, string fullName, string teamId)
			: this()
		{
			Id = id;
			FullName = fullName;
			TeamId = teamId;
		}

		public string Id { get; set; }

		public string FullName { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string TeamId { get; set; }

		public string Team { get; set; }

		public decimal Fpp
		{
			get { return Fpps.Count > 0 ? Fpps[0] : 0; }
			set
			{
				if (Fpps.Count > 0)
				{
					Fpps[0] = value;
				}
				else
				{
					Fpps.Add(value);
				}
			}
		}

		public decimal Salary
		{
			get { return Salaries.Count > 0 ? Salaries[0] : 0; }
			set
			{
				if (Salaries.Count > 0)
				{
					Salaries[0] = value;
				}
				else
				{
					Salaries.Add(value);
				}
			}
		}

		public List<decimal> Fpps { get; private set; }

		public List<decimal> Salaries { get; private set; }

		public List<string> Positions { get; set; }

        public List<string> OriginalPositions { get; set; }
	}
}
