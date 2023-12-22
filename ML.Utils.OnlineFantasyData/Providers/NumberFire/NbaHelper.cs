using ML.Utils.OnlineFantasyData.Entities;
using ML.Utils.OnlineFantasyData.Providers.NumberFire.NBA;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace ML.Utils.OnlineFantasyData.Providers.NumberFire
{
	internal sealed class NbaHelper : __BaseHelperByJs
	{
		public NbaHelper(NumberFireProvider provider)
			: base(provider)
		{
		}

		#region Daily Projections: https://www.numberfire.com/nba/fantasy/fantasy-basketball-projections/{position}

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

					foreach (var position in new[] { "guards", "forwards", "centers" })
					{
						var content = client.DownloadString("https://www.numberfire.com/nba/fantasy/fantasy-basketball-projections/" + position);

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
								json.Add("team_analytics", jsonPosition["team_analytics"]);
								//json.Add("related_articles", jsonPosition["related_articles"]);
								firstLoaded = true;
							}

							jsonPosition.Remove("is_logged_in");
							jsonPosition.Remove("FACEBOOK_APP_NAMESPACE");
							jsonPosition.Remove("FACEBOOK_APP_ID");
							jsonPosition.Remove("page_sport_name");
							jsonPosition.Remove("teams");
							jsonPosition.Remove("team_analytics");
							jsonPosition.Remove("related_articles");

							var dailyProjections = jsonPosition["daily_projections"];

							if (dailyProjections != null)
							{
								jsonPosition.Add("projections", dailyProjections);
								jsonPosition.Remove("daily_projections");
							}

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
			return string.Format("NBA_DailyProjections_{0:yyyyMMdd}.json", estDate ?? Provider.LocalNow);
		}

		#endregion

		#region Daily Lineups: https://www.numberfire.com/nba/daily-fantasy

		public override List<Player> GeMostUsedPlayers(DateTime? estDate = null)
		{
			return new List<Player>();
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
						var content = client.UploadString(new Uri("https://www.numberfire.com/nba/daily-fantasy/get-dashboard"), "s=" + site + "&d=" + Provider.LocalNow.ToString("yyyy-MM-dd"));
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
			return string.Format("NBA_DailyLineups_{0:yyyyMMdd}.json", estDate ?? Provider.LocalNow);
		}

		#endregion

		protected override string MapPlayerPosition(string position)
		{
			switch (position.ToUpper())
			{
				default:
					return position;
			}
		}
	}
}