namespace ML.Utils.Phone.Vendors.Plivo.Models
{
	public class InboundVoicePricing
	{
		public SipInboundPricing ip { get; set; }
		public LocalInboundPricing local { get; set; }
		public TollfreeInboundPricing tollfree { get; set; }
	}
}