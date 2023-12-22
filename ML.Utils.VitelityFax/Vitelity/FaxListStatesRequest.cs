
using ML.Utils.VitelityFax.Base;

namespace ML.Utils.VitelityFax.Vitelity
{
    public class FaxListStatesRequest : BaseVitelityRequest
    {
        public FaxListStatesRequest()
        {
            CmdName = "faxliststates";
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
