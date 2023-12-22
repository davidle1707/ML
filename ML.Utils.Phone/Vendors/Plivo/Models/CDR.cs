namespace ML.Utils.Phone.Vendors.Plivo.Models
{
	public class CDR
	{
		public int bill_duration { get; set; }
		public string total_amount { get; set; }
		public string parent_call_uuid { get; set; }
		public string call_direction { get; set; }
		public string to_number { get; set; }
		public string total_rate { get; set; }
		public string from_number { get; set; }
		public string end_time { get; set; }
		public string call_uuid { get; set; }
		public string resource_uri { get; set; }
		public string error { get; set; }
		public string api_id { get; set; }
	}
}