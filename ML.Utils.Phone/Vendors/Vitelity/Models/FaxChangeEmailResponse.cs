﻿using ML.Utils.Phone.Vendors.Vitelity.Base;

namespace ML.Utils.Phone.Vendors.Vitelity.Models
{
    public class FaxChangeEmailResponse : BaseResponse
    {
        public class FaxListMyDidsResponse : BaseResponse
        {
            public string Response { get; set; }

            internal new void Parse(VitelityResponseXml xmlResponse)
            {
                base.Parse(xmlResponse);
                if (Success)
                {
                    Response = xmlResponse.SelectSingleNode(@"content/response").GetText();
                }
            }
        }
    }
}