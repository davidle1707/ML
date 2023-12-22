using System;
using Newtonsoft.Json;

namespace ML.Utils.VeratadIDresponse
{
	[Serializable]
	public abstract class BaseRequest
	{
		[JsonProperty("fn", NullValueHandling = NullValueHandling.Ignore)]
		public string FirstName { get; set; }

		[JsonProperty("ln", NullValueHandling = NullValueHandling.Ignore)]
		public string LastName { get; set; }

		[JsonProperty("addr", NullValueHandling = NullValueHandling.Ignore)]
		public string Addresss { get; set; }

		[JsonProperty("city", NullValueHandling = NullValueHandling.Ignore)]
		public string City { get; set; }

		[JsonProperty("state", NullValueHandling = NullValueHandling.Ignore)]
		public string State { get; set; }

		[JsonProperty("zip", NullValueHandling = NullValueHandling.Ignore)]
		public string Zip { get; set; }

		[JsonProperty("dob", NullValueHandling = NullValueHandling.Ignore)]
		internal string InternalDob { get { return Dob != null ? Dob.Value.ToString("yyyyMMdd") : null; } }

		[JsonProperty("dob_type", NullValueHandling = NullValueHandling.Ignore)]
		internal string InternalDobTye { get { return "YYYYMMDD"; } }

		[JsonIgnore]
		public DateTime? Dob { get; set; }

		/// <summary>
		/// Age: 27. Age Range: 18-27 or 18+ or 55-
		/// </summary>
		[JsonProperty("age", NullValueHandling = NullValueHandling.Ignore)]
		public string Age { get; set; }

		[JsonProperty("ssn", NullValueHandling = NullValueHandling.Ignore)]
		public string Ssn { get; set; }

		[JsonProperty("dcalc_mask", NullValueHandling = NullValueHandling.Ignore)]
		public string IDRCalcMask { get; set; }

		[JsonProperty("dcalc_answer", NullValueHandling = NullValueHandling.Ignore)]
		public string IDRCalcAnswer { get; set; }
	}
}
