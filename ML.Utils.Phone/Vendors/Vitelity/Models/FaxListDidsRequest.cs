
using ML.Utils.Phone.Vendors.Vitelity.Base;

namespace ML.Utils.Phone.Vendors.Vitelity.Models
{
    public class FaxListDidsRequest : BaseVitelityRequest
    {
        public FaxListDidsRequest()
        {
            CmdName = "faxlistdids";
        }

        public string State { get; set; }

        public override string QueryString
        {
            get
            {
                ResetParam();
                AddParam("cmd", CmdName);
                AddParam("state", State);
                AddParam("type", "base64");

                return ParamsToQueryString();
            }
        }
    }
}
