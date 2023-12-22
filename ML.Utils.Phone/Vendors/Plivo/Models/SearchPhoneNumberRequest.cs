
namespace ML.Utils.Phone.Vendors.Plivo.Models
{
	public class SearchPhoneNumberRequest
	{
		public SearchPhoneNumberRequest()
		{
			country_iso = "US";

			type = "tollfree";

			limit = 20;

			offset = 0;
		}

		/// <summary>
		/// Mandotory. Defaults to US, The ISO code A2 of the country ( BE for Belgium, DE for Germany, GB for United Kingdom, US for United States etc ). View <see cref="http://en.wikipedia.org/wiki/ISO_3166-1_alpha-2"/> for a list of ISOs for different countries.
		/// </summary>
		public string country_iso { get; set; }

        /// <summary>
        /// This filter is only applicable when the type is fixed. If the type is not provided, it is assumed to be fixed. Region based filtering can be performed with the following terms: Exact names of the region: You could use region = Frankfurt if you were looking for a number in Frankfurt.Performed if the search term is three or more characters in length.
        /// </summary>
        public string region { get; set; }

        /// <summary>
        /// (fixed; mobile; tollfree; any) Defaults to tollfree if this field is not specified. type also accepts the value any, which will search for all 3 number types.
        /// </summary>
        public string type { get; set; }

		/// <summary>
		/// (voice; voice,sms; sms) Filters out phone numbers according to the services you want from that number.
		/// </summary>
		public string services { get; set; }

		/// <summary>
		/// Represents the pattern of the number to be searched. Adding a pattern will search for numbers which start with the country code + pattern. For eg. a pattern of 415 and a country_iso: US will search for numbers starting with 1415.
		/// </summary>
		public string pattern { get; set; }

		/// <summary>
		/// Used to display the number of results per page. The maximum number of results that can be fetched is 20.
		/// </summary>
		public int? limit { get; set; }

		/// <summary>
		/// Denotes the number of value items by which the results should be offset. Eg:- If the result contains a 1000 values and limit is set to 10 and offset is set to 705, then values 706 through 715 are displayed in the results. This parameter is also used for pagination of the results
		/// </summary>
		public int? offset { get; set; }
	}
}
