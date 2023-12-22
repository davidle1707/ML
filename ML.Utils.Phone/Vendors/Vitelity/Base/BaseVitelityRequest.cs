using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Linq;

namespace ML.Utils.Phone.Vendors.Vitelity.Base
{
    public abstract class BaseVitelityRequest
    {
        public string CmdName { get; protected set; }
        
        private Dictionary<string, string> Parameters { get; set; }

        public abstract string QueryString { get; }

        protected void ResetParam()
        {
            Parameters = new Dictionary<string, string>();
        }

        protected void AddParam(string key, string value)
        {
            if (!string.IsNullOrEmpty(value) && !Parameters.ContainsValue(key))
            {
                if (!Parameters.ContainsValue(key))
                {
                    Parameters.Add(key, value);
                }
                else
                {
                    Parameters[key] = value;
                }
            }
        }

        protected string ParamsToQueryString()
        {
            AddParam("xml", "yes");

            var builder = Parameters.Select(p => $"{p.Key}={HttpUtility.UrlEncode(p.Value)}");
            var queryString = string.Join("&", builder);

            return HttpUtility.UrlPathEncode(queryString);
        }
    }
}