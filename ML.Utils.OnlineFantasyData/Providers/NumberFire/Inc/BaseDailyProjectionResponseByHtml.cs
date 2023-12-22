using System;
using System.Collections.Generic;
using ML.Utils.OnlineFantasyData.Providers.NumberFire.Inc;

namespace ML.Utils.OnlineFantasyData.Providers.NumberFire.NFL.Inc
{
	[Serializable]
	public class BaseDailyProjectionResponseByHtml<T> : BaseDailyProjectionResponse
		where T : BaseDailyProjectionByHtml
	{
		public BaseDailyProjectionResponseByHtml()
		{
			projections = new List<T>();
		}

		public List<T> projections { get; set; }

		public override bool IsValid
		{
			get { return projections != null && projections.Count > 0; }
		}
	}
}
