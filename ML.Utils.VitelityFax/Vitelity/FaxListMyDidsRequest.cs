using ML.Utils.VitelityFax.Base;

namespace ML.Utils.VitelityFax.Vitelity
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
