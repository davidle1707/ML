using ML.Utils.VitelityFax.Base;

namespace ML.Utils.VitelityFax.Vitelity
{
    public class SendFaxResponse : BaseResponse
    {
        public int JobId { get; set; }

        internal new void Parse(VitelityResponseXml xmlResponse)
        {
            base.Parse(xmlResponse);
            if (Success)
            {
                JobId = xmlResponse.SelectSingleNode(@"content/jobid").GetInt();
            }
        }
    }
}
