using System.Linq;
using ML.Utils.OnlineFantasyData.Entities;
using ML.Utils.OnlineFantasyData.Providers.NumberFire;
using System;
using System.Collections.Generic;
using System.Threading;

namespace ML.Utils.OnlineFantasyData.Providers
{
	public class NumberFireProvider : OnlineDataProvider
	{
		public NumberFireProvider(params int[] siteRefers)
			: base(typeof(NumberFireProvider))
		{
			if (siteRefers == null || siteRefers.Length == 0)
			{
				siteRefers = new[] { NumberFireSiteRefer.DraftKings };
			}

			SiteRefers = new List<int>(siteRefers.Distinct());
		}

		internal List<int> SiteRefers { get; set; }

		internal override string ProviderName { get { return typeof(NumberFireProvider).Name; } }

		internal override List<Player> GetMostUsedPlayers(string sport, DateTime? estDate = null)
		{
			var players = new List<Player>();

			ProcessHelper(sport, helper => players.AddRange(helper.GeMostUsedPlayers(estDate)));

			return players;
		}

		internal override List<ProjectedPlayer> GetProjectedPlayers(string sport, DateTime? estDate = null)
		{
			var players = new List<ProjectedPlayer>();

			ProcessHelper(sport, helper => players.AddRange(helper.GetProjections(estDate)));

			return players;
		}

		internal override void Download(string sport, DateTime? estDate = null)
		{
			ProcessHelper(sport, helper =>
			{
				//Daily Projections
				helper.DownloadProjections(estDate);

				Thread.Sleep(200);

				//Daily Lineups
				helper.DownloadLineups(estDate);
			});
		}

		private void ProcessHelper(string sport, Action<__BaseHelper> action)
		{
			__BaseHelper helper = null;

			switch (sport.ToUpper())
			{
				case "MLB":
					helper = new MlbHelper(this);
					break;

				case "NFL":
					helper = new NflHelper(this);
					break;

				case "NBA":
					helper = new NbaHelper(this);
					break;

				case "NHL":
					helper = new NhlHelper(this);
					break;
			}

			if (helper != null && action != null)
			{
				action(helper);
			}
		}
	}
}
