using System;
using ML.Utils.Phone.Vendors.Vitelity.Models;

namespace ML.Utils.Phone.Vendors.Vitelity.Base
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
                ErrorMessage = string.Join(";", xmlResponse.Errors);
            }
        }
    }
}
