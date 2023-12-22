using System.Text.RegularExpressions;
using HtmlAgilityPack;
using ML.Utils.OnlineFantasyData.Entities;
using ML.Utils.OnlineFantasyData.Providers.NumberFire.NHL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using ML.Common;

namespace ML.Utils.OnlineFantasyData.Providers.NumberFire
{
	internal sealed class NhlHelper : __BaseHelperByHtml
	{
		public NhlHelper(NumberFireProvider provider)
			: base(provider)
		{
		}

		#region Daily Projections: https://www.numberfire.com/nhl/daily-fantasy-hockey-projections/{position}

		public override List<ProjectedPlayer> GetProjections(DateTime? estDate = null)
		{
			return GetProjectionsByHtml<DailyProjectionResponse, DailyProjection>(estDate);
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
					var html = new HtmlDocument();

					var players = new List<DailyProjection>();

					foreach (var position in new[] { "skaters", "goalies", "forwards", "c", "lw", "rw", "d" })
					{
						var content = client.DownloadString("https://www.numberfire.com/nhl/daily-fantasy-hockey-projections/" + position);

						if (string.IsNullOrWhiteSpace(content))
						{
							continue;
						}

						html.LoadHtml(content);

						var tbody = html.DocumentNode.SelectSingleNode("//tbody[@id='projection-data']");

						if (tbody != null)
						{
							foreach (var tr in tbody.SelectNodes("tr"))
							{
								var tds = tr.SelectNodes("td").Where(td => td.Attributes.Any(a => a.Name == "class") && td.InnerText != "&nbsp;").ToList();

								if (tds.Count == 0)
								{
									continue;
								}

								var player = GetBaseInfoDailyProjection(tds[0].InnerText);

								if (player == null || string.IsNullOrEmpty(player.player) || players.Any(p => p.EqualsString(player.player)))
								{
									continue;
								}

								player.opponent = tds[1].InnerText;

								player.numberfire_fp = GetSiteReferValue(tds, "nf", "col-fp");

								player.fanduel_fp = GetSiteReferValue(tds, "fanduel", "col-site_fp");
								player.fanduel_salary = GetSiteReferValue(tds, "fanduel", "col-site_salary").Replace("$", "");
								player.fanduel_ratio = GetSiteReferValue(tds, "fanduel", "col-site_ratio");

								player.draft_kings_fp = GetSiteReferValue(tds, "draft_kings", "col-site_fp");
								player.draft_kings_salary = GetSiteReferValue(tds, "draft_kings", "col-site_salary").Replace("$", "");
								player.draft_kings_ratio = GetSiteReferValue(tds, "draft_kings", "col-site_ratio");

								player.fantasy_feud_fp = GetSiteReferValue(tds, "fantasy_feud", "col-site_fp");
								player.fantasy_feud_salary = GetSiteReferValue(tds, "fantasy_feud", "col-site_salary").Replace("$", "");
								player.fantasy_feud_ratio = GetSiteReferValue(tds, "fantasy_feud", "col-site_ratio");

								player.draftday_fp = GetSiteReferValue(tds, "draftday", "col-site_fp");
								player.draftday_salary = GetSiteReferValue(tds, "draftday", "col-site_salary").Replace("$", "");
								player.draftday_ratio = GetSiteReferValue(tds, "draftday", "col-site_ratio");

								player.fantasy_aces_fp = GetSiteReferValue(tds, "fantasy_aces", "col-site_fp");
								player.fantasy_aces_salary = GetSiteReferValue(tds, "fantasy_aces", "col-site_salary").Replace("$", "");
								player.fantasy_aces_ratio = GetSiteReferValue(tds, "fantasy_aces", "col-site_ratio");

								player.draftster_fp = GetSiteReferValue(tds, "draftster", "col-site_fp");
								player.draftster_salary = GetSiteReferValue(tds, "draftster", "col-site_salary").Replace("$", "");
								player.draftster_ratio = GetSiteReferValue(tds, "draftster", "col-site_ratio");

								player.fantasy_score_fp = GetSiteReferValue(tds, "fantasy_score", "col-site_fp");
								player.fantasy_score_salary = GetSiteReferValue(tds, "fantasy_score", "col-site_salary").Replace("$", "");
								player.fantasy_score_ratio = GetSiteReferValue(tds, "fantasy_score", "col-site_ratio");

								player.victiv_fp = GetSiteReferValue(tds, "victiv", "col-site_fp");
								player.victiv_salary = GetSiteReferValue(tds, "victiv", "col-site_salary").Replace("$", "");
								player.victiv_ratio = GetSiteReferValue(tds, "victiv", "col-site_ratio");

								player.yahoo_fp = GetSiteReferValue(tds, "yahoo", "col-site_fp");
								player.yahoo_salary = GetSiteReferValue(tds, "yahoo", "col-site_salary").Replace("$", "");
								player.yahoo_ratio = GetSiteReferValue(tds, "yahoo", "col-site_ratio");

								players.Add(player);
							}
						}
					}

					json.Add("projections", JToken.FromObject(players));

					var jsonAsString = JsonConvert.SerializeObject(json);

					if (Provider.StoreToFile)
					{
						Provider.WriteToFile(fileKey, jsonAsString);
					}

					return jsonAsString;
				}
			}
			catch (Exception ex)
			{
				Provider.Log.Error(ex);
			}

			return string.Empty;
		}

		protected override string GetProjectionFileKey(DateTime? estDate = null)
		{
			return string.Format("NHL_DailyProjections_{0:yyyyMMdd}.json", estDate ?? Provider.LocalNow);
		}

		//Ex: Logan Thomas (QB, ARI) -> Player (Logan Thomas) - Position (QB) - Team (ARI)
		private DailyProjection GetBaseInfoDailyProjection(string nodeText)
		{
			if (string.IsNullOrEmpty(nodeText))
			{
				return null;
			}

			var player = new DailyProjection { player = Regex.Replace(nodeText, @"\((.+?)\)", "").Trim() };

			var regTeamAndPosition = Regex.Match(nodeText, @"\((.+?)\)");

			if (regTeamAndPosition.Success)
			{
				var teamAndPoistion = regTeamAndPosition.Groups[1].Value.Split(',');
				player.position = teamAndPoistion[0].Trim();
				player.team = teamAndPoistion.Length > 1 ? teamAndPoistion[1].Trim() : string.Empty;
			}

			return player;
		}

		private string GetSiteReferValue(IEnumerable<HtmlNode> tds, string cssSiteName, string cssFieldName)
		{
			var td = tds.FirstOrDefault(t => t.Attributes.Any(a => a.Name == "class" && a.Value.Contains(cssSiteName) && a.Value.Contains(cssFieldName)));
			return (td != null ? td.InnerText : string.Empty).ToStr();
		}

		#endregion

		#region Daily Lineups: https://www.numberfire.com/nhl/daily-fantasy

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
						var content = client.UploadString(new Uri("https://www.numberfire.com/nhl/daily-fantasy/daily-fantasy/get-dashboard"), "s=" + site + "&d=" + Provider.LocalNow.ToString("yyyy-MM-dd"));
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
			return string.Format("NHL_DailyLineups_{0:yyyyMMdd}.json", estDate ?? Provider.LocalNow);
		}

		#endregion

		protected override string MapPlayerPosition(string position)
		{
			switch (position.ToUpper())
			{
				case "LW":
				case "RW":
					return "W";

				default:
					return position;
			}
		}
	}
}