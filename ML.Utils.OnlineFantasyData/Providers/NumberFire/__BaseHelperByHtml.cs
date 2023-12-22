using ML.Common;
using ML.Utils.OnlineFantasyData.Entities;
using ML.Utils.OnlineFantasyData.Providers.NumberFire.NFL.Inc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ML.Utils.OnlineFantasyData.Providers.NumberFire
{
	internal abstract class __BaseHelperByHtml : __BaseHelper
	{
		protected __BaseHelperByHtml(NumberFireProvider provider)
			: base(provider)
		{
		}

		protected virtual List<ProjectedPlayer> GetProjectionsByHtml<TDailyProjectionResponseByHtml, TDailyProjection>(DateTime? estDate = null)
			where TDailyProjectionResponseByHtml : BaseDailyProjectionResponseByHtml<TDailyProjection>, new()
			where TDailyProjection : BaseDailyProjectionByHtml
		{
			var projectedPlayers = new List<ProjectedPlayer>();

			var response = GetProjectionResponse<TDailyProjectionResponseByHtml>(estDate);

			if (response == null || !response.IsValid)
			{
				return projectedPlayers;
			}

			foreach (var item in response.projections)
			{
				var projectedPlayer = projectedPlayers.SingleOrDefault(p => p.FullName.EqualsString(item.player));

				if (projectedPlayer == null)
				{
					projectedPlayer = GetProjectedPlayerByHtml(item);
					projectedPlayers.Add(projectedPlayer);
				}

				projectedPlayer.Positions = projectedPlayer.Positions.Union(GetPlayerPositions(item.position)).ToList();
                projectedPlayer.OriginalPositions = projectedPlayer.Positions.Union(GetPlayerOriginalPositions(item.position)).ToList();
			}

			PopulateBestProjections(projectedPlayers);

			return projectedPlayers;
		}

		private ProjectedPlayer GetProjectedPlayerByHtml(BaseDailyProjectionByHtml projection)
		{
			var playerId = string.Format("{0}_{1}", projection.team.ToLower(), projection.player.ToLower());

			var projectedPlayer = new ProjectedPlayer(playerId, projection.player, projection.team)
			{
				Team = projection.team,
				OpponentId = projection.opponent,
				Opponent = projection.opponent,
			};

			PopulateSiteReferInfo(projectedPlayer, projection);

			return projectedPlayer;
		}
	}
}
