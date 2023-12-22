using System;
using System.Collections.Generic;
using System.Linq;

namespace ML.Common
{
    public static class UrlStringUtil
    {
        public static Dictionary<string, string> GetUrlParams(this string url)
        {
	        var urlparams = new Dictionary<string, string>();

	        if (!string.IsNullOrWhiteSpace(url))
	        {
		        try
		        {
					Uri currentUrl;

			        if (Uri.TryCreate(url, UriKind.Absolute, out currentUrl))
					{
						if (!string.IsNullOrEmpty(currentUrl.Query))
						{
							var queryAndValues = currentUrl.Query.Replace("?", "").Split('&');
							
							urlparams = queryAndValues.Select(qv => qv.Split('=')).ToDictionary(items => items[0], items => items[1]);
						}
					}
		        }
		        catch { } 
	        }
			
			return urlparams;
        }

        public static string GetUrlParamValue(this string url, string paramName)
        {
            var urlParams = url.GetUrlParams();

			return urlParams.ContainsKey(paramName) ? urlParams[paramName] : string.Empty;
        }
    }
}
