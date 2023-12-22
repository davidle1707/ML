using System.Collections.Generic;

namespace ML.Utils.Phone.Vendors.Plivo.Models
{
	public class OutboundVoicePricing
	{
		public SipOutboundPricing ip { get; set; }
		public LocalOutboundPricing local { get; set; }
		public TollfreeOutboundPricing tollfree { get; set; }
		public List<RatesPrefixes> rates { get; set; }
	}
}