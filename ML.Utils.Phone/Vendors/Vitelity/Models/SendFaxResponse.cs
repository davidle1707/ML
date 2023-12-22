using ML.Utils.Phone.Vendors.Vitelity.Base;

namespace ML.Utils.Phone.Vendors.Vitelity.Models
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
