
using ML.Utils.VitelityFax.Base;

namespace ML.Utils.VitelityFax.Vitelity
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
