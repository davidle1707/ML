using ML.Common;
using ML.Utils.OnlineFantasyData.Entities;
using ML.Utils.OnlineFantasyData.Providers.NumberFire.MLB;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace ML.Utils.OnlineFantasyData.Providers.NumberFire
{
	internal sealed class MlbHelper : __BaseHelperByJs
	{
		public MlbHelper(NumberFireProvider provider)
			: base(provider)
		{
		}

		#region Daily Projections: https://www.numberfire.com/mlb/fantasy/fantasy-baseball-projections/{position}

		public override List<ProjectedPlayer> GetProjections(DateTime? estDate = null)
		{
			return GetProjectionsByJs<DailyProjectionResponse, DailyProjection>(estDate);
		}

		public override void DownloadProjections(DateTime? estDate = null)
		{
			if (!IsDaily(estDate))
			{
				return;
			}

			var fileKey = GetProjectionFileKey(estDate);

			DownloadProjectionsFromProvider(fileKey);	
		}

		protected override string DownloadProjectionsFromProvider(string fileKey)
		{
			try
			{
				var json = new JObject();

				using (var client = new WebClient())
				{
					var jsonPositions = new JObject();
					var firstLoaded = false;
					var html = new HtmlAgilityPack.HtmlDocument();

					foreach (var position in new[] { "1B", "2B", "3B", "SS", "OF", "DH", "C", "SP", "RP" })
					{
						var content = client.DownloadString("https://www.numberfire.com/mlb/fantasy/fantasy-baseball-projections/" + position);

						if (string.IsNullOrWhiteSpace(content))
						{
							continue;
						}

						html.LoadHtml(content);

						var tagScript = html.DocumentNode.SelectNodes("//head/script").FirstOrDefault(n => n.InnerText.Contains("var NF_DATA"));

						if (tagScript == null || string.IsNullOrWhiteSpace(tagScript.InnerText))
						{
							continue;
						}

						var nfData = tagScript.InnerText.Trim().Split('\n').FirstOrDefault(c => c.StartsWith("var NF_DATA"));

						if (!string.IsNullOrWhiteSpace(nfData))
						{
							var jsonPosition = (JObject)Provider.DeserializeFromJson(nfData.Replace("var NF_DATA", "").Trim(new[] { ' ', '=', ';' }));

							if (!firstLoaded)
							{
								json.Add("teams", jsonPosition["teams"]);
								//json.Add("lineups", jsonPosition["lineups"]);
								//json.Add("related_articles", jsonPosition["related_articles"]);
								firstLoaded = true;
							}

							jsonPosition.Remove("is_logged_in");
							jsonPosition.Remove("FACEBOOK_APP_NAMESPACE");
							jsonPosition.Remove("FACEBOOK_APP_ID");
							jsonPosition.Remove("page_sport_name");
							jsonPosition.Remove("lineups");
							jsonPosition.Remove("teams");
							jsonPosition.Remove("related_articles");

							jsonPositions.Add(position.ToLower(), jsonPosition);
						}
					}

					json.Add("positions", jsonPositions);
				}

				var jsonAsString = JsonConvert.SerializeObject(json);

				if (Provider.StoreToFile)
				{
					Provider.WriteToFile(fileKey, jsonAsString);
				}

				return jsonAsString;
			}
			catch (Exception ex)
			{
				Provider.Log.Error(ex);
			}

			return string.Empty;
		}

		protected override string GetProjectionFileKey(DateTime? estDate = null)
		{
			return string.Format("MLB_DailyProjections_{0:yyyyMMdd}.json", estDate ?? Provider.LocalNow);
		}

		#endregion

		#region Daily Lineups: https://www.numberfire.com/mlb/daily-fantasy

		public override List<Player> GeMostUsedPlayers(DateTime? estDate = null)
		{
			var mostUsedPlayers = new List<Player>();

			var response = GetLineupResponse(estDate);

			if (response == null || !response.IsValid)
			{
				return mostUsedPlayers;
			}

			foreach (var siteLineup in response.site_lineups.Where(sl => sl.Value != null && sl.Value.IsValidMostUserdPlayers))
			{
				var query = from summary in siteLineup.Value.most_used_players.summaries
							join projection in siteLineup.Value.projections on summary.player_id equals projection.Key
							join player in siteLineup.Value.most_used_players.players.Values on summary.player_id equals player.id
							select new { projection, player };

				foreach (var item in query)
				{
					if (mostUsedPlayers.All(p => p.Id != item.player.id))
					{
						mostUsedPlayers.Add(new Player
						{
							Id = item.player.id,
							FullName = item.player.name,
							FirstName = item.player.first_name,
							LastName = item.player.last_name,
							Positions = GetPlayerPositions(item.player.position).ToList(),
							Fpp = item.projection.Value.ToDecimal()
						});
					}
				}
			}

			return mostUsedPlayers;
		}

		public override void DownloadLineups(DateTime? estDate = null)
		{
			if (!IsDaily(estDate))
			{
				return;
			}

			var fileKey = GetLineupFileKey(estDate);

			DownloadLineupsFromProvider(fileKey);
		}
		
		protected override string DownloadLineupsFromProvider(string fileKey)
		{
			try
			{
				var json = new JObject();

				using (var client = new WebClient())
				{
					var jsonSites = new JObject();

					var referSites = new[] { "draft_kings", "fanduel", "fantasy_feud", "draftday", "fantasy_aces", "draftster" };

					foreach (var site in referSites)
					{
						client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
						var content = client.UploadString(new Uri("https://www.numberfire.com/mlb/daily-fantasy/get-dashboard"), "s=" + site);
						jsonSites.Add(site, (JObject)Provider.DeserializeFromJson(content));
					}

					json.Add("site_lineups", jsonSites);
				}

				var jsonAsString = JsonConvert.SerializeObject(json);

				if (Provider.StoreToFile)
				{
					Provider.WriteToFile(fileKey, jsonAsString);
				}

				return jsonAsString;
			}
			catch (Exception ex)
			{
				Provider.Log.Error(ex);
			}

			return string.Empty;
		}

		protected override string GetLineupFileKey(DateTime? estDate = null)
		{
			return string.Format("MLB_DailyLineups_{0:yyyyMMdd}.json", estDate ?? Provider.LocalNow);
		}

		private DailyLineupResponse GetLineupResponse(DateTime? estDate = null)
		{
			string json = null;
			var fileKey = GetLineupFileKey(estDate);

			if (Provider.StoreToFile)
			{
				json = Provider.ReadFromFile(fileKey);
			}

			if (string.IsNullOrWhiteSpace(json) && (estDate == null || estDate.Value.Date != Provider.LocalNow.Date))
			{
				json = DownloadLineupsFromProvider(fileKey);
			}

			var response = Provider.DeserializeFromJson<DailyLineupResponse>(json);

			return response;
		}


		#endregion

		protected override string MapPlayerPosition(string position)
		{
			switch (position.ToUpper())
			{
				case "RP":
				case "SP":
				case "CL":
					return "P";
				case "RF":
				case "LF":
				case "CF":
					return "OF";
				default:
					return position;
			}
		}
	}
}
