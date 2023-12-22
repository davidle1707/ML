using ML.Common;

namespace ML.Utils.IpLookup.Providers
{
    /*
	 * 
     * Site: http://freegeoip.net/
	 * 
     * API Lookup: http://freegeoip.net/{json|xml}/{ip}
	 * 
     */
    public class FreegeoIpProvider : IpLookupProvider
    {
        internal override IpInfo Lookup(string ip)
        {
            var responseAsJson = HttpHelper.Get(string.Format("http://freegeoip.net/json/{0}", ip));

            if (string.IsNullOrWhiteSpace(responseAsJson))
            {
				return null;
            }

            var response = std.DeserializeJson(responseAsJson);

            if (response == null || response.Count == 0)
            {
				return null;
            }

	        return new IpInfo
	               {
		               Ip = ip,
		               Country = response.Value<string>("country_name"),
		               CountryCode = response.Value<string>("country_code"),
		               Region = response.Value<string>("region_name"),
		               RegionCode = response.Value<string>("region_code"),
		               City = response.Value<string>("city"),
					   ByProvider = typeof(FreegeoIpProvider).Name
	               };
        }
    }
}
