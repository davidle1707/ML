using System;
using System.Collections.Generic;

namespace ML.Utils.IntegrityDirect
{
	[Serializable]
	public class IdentityMatchResponse
	{
		public IdentityMatchResponse()
		{
			MatchDescriptions = new List<MatchDescription>();
		}

		public string TransactionId { get; internal set; }

		public string MatchCode { get; internal set; }

		public string MatchDescription { get; internal set; }

		public List<MatchDescription> MatchDescriptions  { get; internal set; }
	
		public string ErrorCode { get; internal set; }

		public string ErrorDescription { get; internal set; }
	}
}
