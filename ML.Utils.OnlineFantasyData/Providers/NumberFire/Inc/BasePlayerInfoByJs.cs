using System;

namespace ML.Utils.OnlineFantasyData.Providers.NumberFire.Inc
{
	[Serializable]
	public class BasePlayerInfoByJs
	{
		public string id { get; set; }
		public string name { get; set; }
		public string slug { get; set; }
		public string first_name { get; set; }
		public string last_name { get; set; }
		public string race { get; set; }
		public string position { get; set; }
		public string position_group { get; set; }
		public string team_id { get; set; }
		public string number { get; set; }
		public string height { get; set; }
		public string weight { get; set; }
		public string age { get; set; }
		public string experience { get; set; }
		public string dob { get; set; }
		public string url { get; set; }
		public string yahoo_id { get; set; }
		public string espn_id { get; set; }
		public string league_id { get; set; }
		public string sports_reference_id { get; set; }
		public string role_id { get; set; }
		public string category_id { get; set; }
		public string college { get; set; }
		public string is_active { get; set; }
		public string is_rookie { get; set; }
		public string depth_position { get; set; }
		public string depth_rank { get; set; }
		public string bats { get; set; }
		public string throws { get; set; }
	}
}
