namespace ML.Utils.Phone.Vendors.Plivo.Models
{
	public class OutgoingCarrierRouting
	{
		public string routing_id { get; set; }
		public string digits { get; set; }
		public int priority { get; set; }
		public string outgoing_carrier { get; set; }
		public string resource_uri { get; set; }
	}
}