using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ML.Common;
using Newtonsoft.Json.Linq;

namespace ML.Utils.ZipLookup.Providers
{
	/*
	 * Url: https://developers.google.com/maps/documentation/geocoding/?csw=1
	 * ZipLookup Url: http://maps.googleapis.com/maps/api/geocode/json?address=[zip_code]
	 */
	public class GoogleZipLookupProvider : ZipLookupProvider
	{
        internal override ZipInfo Lookup(string zip, string country = "")
		{
            var url = GetUrl(zip, country);

            var responseAsJson = HttpHelper.Get(url);

            return ExecuteLookup(zip, responseAsJson);
		}

        internal override async Task<ZipInfo> LookupAsync(string zip, string country = "")
        {
            var url = GetUrl(zip, country);

            var responseAsJson = await HttpHelper.GetAsync(url);

            return ExecuteLookup(zip, responseAsJson);
        }

		internal override ZipInfo LookupUS(string zip)
		{
            return Lookup(zip, "US");
		}

        private ZipInfo ExecuteLookup(string zip, string responseAsJson)
		{
			if (string.IsNullOrWhiteSpace(responseAsJson))
			{
				return null;
			}

			var response = std.DeserializeJson(responseAsJson);

			if (response == null)
			{
				return null;
			}

			var firstResult = response["results"].FirstOrDefault();

			if (firstResult == null)
			{
				return null;
			}

			var address = firstResult["address_components"];
			
			return new ZipInfo
			       {
					   City = GetValue(address, "locality"),
					   State = GetValue(address, "administrative_area_level_1"),
					   Zip = zip,
					   Country = GetValue(address, "country"),
					   ByProvider = typeof(GoogleZipLookupProvider).Name
			       };		
		}

        private string GetUrl(string zip, string country = "")
        {
            var url = string.Format("http://maps.googleapis.com/maps/api/geocode/json?address={0}&sensor=false", zip);

            if (!string.IsNullOrWhiteSpace(country))
            {
                url += "&components=country:" + country;
            }

            return url;
        }

		private string GetValue(IEnumerable<JToken> result, string type, string field = "short_name")
		{
			var response = result.FirstOrDefault(r => r["types"].First().ToString() == type);

			return response != null ? response[field].ToString() : null;
		}
	}
}
