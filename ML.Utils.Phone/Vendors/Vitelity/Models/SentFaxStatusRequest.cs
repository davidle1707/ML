
using ML.Utils.Phone.Vendors.Vitelity.Base;

namespace ML.Utils.Phone.Vendors.Vitelity.Models
{
    public class SentFaxStatusRequest : BaseVitelityRequest
    {
        public string ShowXmlStatus { get; set; }

        public SentFaxStatusRequest()
        {
            CmdName = "sentfaxstatus";
            ShowXmlStatus = "yes";
        }
        public override string QueryString
        {
            get
            {
                ResetParam();
                AddParam("cmd", CmdName);
                AddParam("showxmlstatus", ShowXmlStatus);
                AddParam("type", "base64");

                return ParamsToQueryString();
            }
        }
    }
}
