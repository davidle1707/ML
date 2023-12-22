using System;
using ML.Utils.VitelityFax.Vitelity;

namespace ML.Utils.VitelityFax.Base
{
    public class BaseResponse
    {
        public bool Success { get; set; }

        public string ErrorMessage { get; set; }

        public void Parse(VitelityResponseXml xmlResponse)
        {
            Success = xmlResponse.Status == Status.Ok;
            if (xmlResponse.HasErrors)
            {
                ErrorMessage = String.Join(";", xmlResponse.Errors);
            }
        }
    }
}
