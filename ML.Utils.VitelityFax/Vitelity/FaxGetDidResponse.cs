using ML.Utils.VitelityFax.Base;

namespace ML.Utils.VitelityFax.Vitelity
{
    public class FaxGetDidResponse : BaseResponse
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
