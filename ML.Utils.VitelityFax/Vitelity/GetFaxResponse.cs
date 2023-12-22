using System;
using ML.Utils.VitelityFax.Base;

namespace ML.Utils.VitelityFax.Vitelity
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
                    if (!String.IsNullOrEmpty(selectSingleNode.InnerText))
                    {
                        Data = Convert.FromBase64String(selectSingleNode.InnerText);
                    }
                }
            }
        }
    }
}
