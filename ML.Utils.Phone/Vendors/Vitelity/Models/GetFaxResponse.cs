using System;
using ML.Utils.Phone.Vendors.Vitelity.Base;

namespace ML.Utils.Phone.Vendors.Vitelity.Models
{
    public class GetFaxResponse: BaseResponse
    {
        public byte[] Data { get; set; }

        internal new void Parse(VitelityResponseXml xmlResponse)
        {
            base.Parse(xmlResponse);
            if (Success)
            {
                var selectSingleNode = xmlResponse.SelectSingleNode(@"content/data");
                if (selectSingleNode != null)
                {
                    if (!string.IsNullOrEmpty(selectSingleNode.InnerText))
                    {
                        Data = Convert.FromBase64String(selectSingleNode.InnerText);
                    }
                }
            }
        }
    }
}
