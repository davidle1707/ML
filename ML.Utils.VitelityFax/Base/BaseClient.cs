using System;
using System.Net;
using ML.Utils.VitelityFax.Vitelity;
using log4net;

namespace ML.Utils.VitelityFax.Base
{
    public class BaseClient
    {
        private readonly ILog _log = LogManager.GetLogger(typeof(VitelityFaxManager));

        public VitelityResponseXml BasePostRequest(BaseVitelityRequest request, Func<WebClient, VitelityResponseXml> requestFunc)
        {
            VitelityResponseXml result = null;

            try
            {
                using (var client = new WebClient())
                {

                    result = requestFunc(client);

                    if (result.HasErrors)
                    {
                        throw new Exception(String.Join(". ", result.Errors));
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                throw new VitelityFaxException(ex.Message, ex);
            }

            return result;
        }
    }
}
