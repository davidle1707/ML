namespace ML.Utils.Phone.Vendors.Plivo.Models
{
	public class PhoneNumber
	{
		public string country { get; set; }
		public int lata { get; set; }
		public string monthly_rental_rate { get; set; }
		public string number { get; set; }
		public string type { get; set; }
		public string prefix { get; set; }
		public string rate_center { get; set; }
		public string region { get; set; }
		public string resource_uri { get; set; }
		public string restriction_text { get; set; }
		public string restriction { get; set; }
		public string setup_rate { get; set; }
		public bool sms_enabled { get; set; }
		public string sms_rate { get; set; }
		public bool voice_enabled { get; set; }
		public string voice_rate { get; set; }
	}
}