using ML.Utils.Phone.Vendors.Vitelity.Base;

namespace ML.Utils.Phone.Vendors.Vitelity.Models
{
    public class FaxListMyDidsRequest : BaseVitelityRequest
    {
        public FaxListMyDidsRequest()
        {
            CmdName = "faxlistmydids";
        }

        public override string QueryString
        {
            get
            {
                ResetParam();
                AddParam("cmd", CmdName);
                AddParam("type", "base64");

                return ParamsToQueryString();
            }
        }
    }
}
