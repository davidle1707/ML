using System;
using ML.Utils.OnlineFantasyData.Providers.NumberFire.Inc;

namespace ML.Utils.OnlineFantasyData.Providers.NumberFire.NFL.Inc
{
    [Serializable]
	public class BaseDailyProjectionByHtml : SiteReferInfo
    {
        public string player { get; set; }
        public string team { get; set; }
        public string position { get; set; }
        public string opponent { get; set; }
    }
}
