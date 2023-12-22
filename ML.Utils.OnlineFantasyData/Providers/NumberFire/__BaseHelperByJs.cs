
using System;
using System.Collections.Generic;
using System.Linq;
using ML.Utils.OnlineFantasyData.Entities;
using ML.Utils.OnlineFantasyData.Providers.NumberFire.Inc;

namespace ML.Utils.OnlineFantasyData.Providers.NumberFire
{
	internal abstract class __BaseHelperByJs : __BaseHelper
	{
		protected __BaseHelperByJs(NumberFireProvider provider)
			: base(provider)
		{

		}

		protected virtual List<ProjectedPlayer> GetProjectionsByJs<TDailyProjectionResponseByJs, TDailyProjection>(DateTime? estDate = null)
			where TDailyProjectionResponseByJs : BaseDailyProjectionResponseByJs<TDailyProjection>, new()
			where TDailyProjection : BaseDailyProjectionByJs
		{
			var projectedPlayers = new List<ProjectedPlayer>();

			var response = GetProjectionResponse<TDailyProjectionResponseByJs>(estDate);

			if (response == null || !response.IsValid)
			{
				return projectedPlayers;
			}

			Func<string, string> getTeamName = tid => response.teams.ContainsKey(tid) ? response.teams[tid].abbrev : string.Empty;

			var query = from projection in response.positions.Values.SelectMany(v => v.projections)
						join player in response.positions.Values.SelectMany(v => v.players.Values) on projection.player_id equals player.id
						select new
						{
							player,
							projection
						};

			foreach (var item in query)
			{
				var projectedPlayer = projectedPlayers.SingleOrDefault(p => p.Id == item.player.id);

				if (projectedPlayer == null)
				{
					projectedPlayer = GetProjectedPlayerByJs(item.player, item.projection, getTeamName);
					projectedPlayers.Add(projectedPlayer);
				}

				projectedPlayer.Positions = projectedPlayer.Positions.Union(GetPlayerPositions(item.player.position)).ToList();
                projectedPlayer.OriginalPositions = projectedPlayer.Positions.Union(GetPlayerOriginalPositions(item.player.position)).ToList();
			}

			PopulateBestProjections(projectedPlayers);

			return projectedPlayers;
		}

		private ProjectedPlayer GetProjectedPlayerByJs(BasePlayerInfoByJs player, BaseDailyProjectionByJs projection, Func<string, string> getTeamName = null)
		{
			var projectedPlayer = new ProjectedPlayer(player.id, player.name, player.team_id)
			{
				FirstName = player.first_name,
				LastName = player.last_name,
				Team = getTeamName != null ? getTeamName(player.team_id) : string.Empty,
				OpponentId = projection.opponent_id,
				Opponent = getTeamName != null ? getTeamName(projection.opponent_id) : string.Empty,
				GameId = projection.game_id
			};

			PopulateSiteReferInfo(projectedPlayer, projection);

			return projectedPlayer;
		}

	}
}
