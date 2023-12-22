using System;
using System.Linq;
using System.Security.Policy;
using ML.Utils.OnlineFantasyData.Entities;
using System.Collections.Generic;
using ML.Common;
using ML.Utils.OnlineFantasyData.Providers.NumberFire.Inc;
using ML.Utils.OnlineFantasyData.Providers.NumberFire.NFL.Inc;

namespace ML.Utils.OnlineFantasyData.Providers.NumberFire
{
	internal abstract class __BaseHelper
	{
		protected readonly NumberFireProvider Provider;

		protected __BaseHelper(NumberFireProvider provider)
		{
			Provider = provider;
		}

		#region Projection

		public abstract List<ProjectedPlayer> GetProjections(DateTime? estDate = null);

		public abstract void DownloadProjections(DateTime? estDate = null);

		protected abstract string DownloadProjectionsFromProvider(string fileKey);

		protected abstract string GetProjectionFileKey(DateTime? estDate = null);
		
		protected TResponse GetProjectionResponse<TResponse>(DateTime? estDate = null) where TResponse : BaseDailyProjectionResponse, new()
		{
			string json = null;
			var fileKey = GetProjectionFileKey(estDate);

			if (Provider.StoreToFile)
			{
				json = Provider.ReadFromFile(fileKey);
			}

			if (string.IsNullOrWhiteSpace(json) && IsDaily(estDate))
			{
				json = DownloadProjectionsFromProvider(fileKey);
			}

			if (string.IsNullOrWhiteSpace(json))
			{
				return Activator.CreateInstance<TResponse>();
			}

			var response = Provider.DeserializeFromJson<TResponse>(json);

			return response;
		}

		protected void PopulateBestProjections(List<ProjectedPlayer> projectedPlayers)
		{
			//set top 15 best players for each position

			projectedPlayers.RemoveAll(p => p.Positions.Count == 0);

			Func<ProjectedPlayer, decimal> bestBy = p => p.Fpp; //p => p.Ratio

			foreach (var group in projectedPlayers.GroupBy(p => p.Positions[0]))
			{
				var playersByPoistions = group.OrderByDescending(bestBy).ThenByDescending(p => p.Salary).Take(15);

				foreach (var player in playersByPoistions)
				{
					player.IsBest = true;
				}
			}
		}

		protected void PopulateSiteReferInfo(ProjectedPlayer projectedPlayer, SiteReferInfo siteReferInfo)
		{
			foreach (var siteRefer in Provider.SiteRefers)
			{
				decimal salary = 0.0m, fpp = 0.0m, ratio = 0.0m;

				switch (siteRefer)
				{
					case NumberFireSiteRefer.FanDuel:
						salary = siteReferInfo.fanduel_salary.ToDecimal();
						fpp = siteReferInfo.fanduel_fp.ToDecimal();
						ratio = siteReferInfo.fanduel_ratio.ToDecimal();
						break;

					case NumberFireSiteRefer.FantasyFeud:
						salary = siteReferInfo.fantasy_feud_salary.ToDecimal();
						fpp = siteReferInfo.fantasy_feud_fp.ToDecimal();
						ratio = siteReferInfo.fantasy_feud_ratio.ToDecimal();
						break;

					case NumberFireSiteRefer.DraftDay:
						salary = siteReferInfo.draftday_salary.ToDecimal();
						fpp = siteReferInfo.draftday_fp.ToDecimal();
						projectedPlayer.Ratio = siteReferInfo.draftday_ratio.ToDecimal();
						break;

					case NumberFireSiteRefer.FantasyAces:
						salary = siteReferInfo.fantasy_aces_salary.ToDecimal();
						fpp = siteReferInfo.fantasy_aces_fp.ToDecimal();
						ratio = siteReferInfo.fantasy_aces_ratio.ToDecimal();
						break;

					case NumberFireSiteRefer.DraftSter:
						salary = siteReferInfo.draftster_salary.ToDecimal();
						fpp = siteReferInfo.draftster_fp.ToDecimal();
						ratio = siteReferInfo.draftster_ratio.ToDecimal();
						break;

					case NumberFireSiteRefer.FantasyScore:
						salary = siteReferInfo.fantasy_score_salary.ToDecimal();
						fpp = siteReferInfo.fantasy_score_fp.ToDecimal();
						ratio = siteReferInfo.fantasy_score_ratio.ToDecimal();
						break;

					case NumberFireSiteRefer.DraftKings:
						salary = siteReferInfo.draft_kings_salary.ToDecimal();
						fpp = siteReferInfo.draft_kings_fp.ToDecimal();
						ratio = siteReferInfo.draft_kings_ratio.ToDecimal();
						break;
				}

				projectedPlayer.Salaries.Add(salary);
				projectedPlayer.Fpps.Add(fpp);
				projectedPlayer.Ratios.Add(ratio);
			}
			
		}

		#endregion

		#region Lineup

		public abstract List<Player> GeMostUsedPlayers(DateTime? estDate = null);

		public abstract void DownloadLineups(DateTime? estDate = null);

		protected abstract string DownloadLineupsFromProvider(string fileKey);

		protected abstract string GetLineupFileKey(DateTime? estDate = null);

		#endregion

		protected abstract string MapPlayerPosition(string position);

		protected IEnumerable<string> GetPlayerPositions(string position)
		{
			return position.Split('/').Select(MapPlayerPosition).Distinct();
		}

        protected IEnumerable<string> GetPlayerOriginalPositions(string position)
        {
            return position.Split('/').AsEnumerable();
        }

		protected bool IsDaily(DateTime? estDate = null)
		{
			return estDate == null || estDate.Value.Date == Provider.LocalNow.Date;
		}
	}
}
