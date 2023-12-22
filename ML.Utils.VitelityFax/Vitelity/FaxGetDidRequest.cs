using ML.Utils.VitelityFax.Base;

namespace ML.Utils.VitelityFax.Vitelity
{
    public class FaxGetDidRequest : BaseVitelityRequest
    {
        public FaxGetDidRequest()
        {
            CmdName = "faxgetdid";
        }

        public string Did { get; set; }

        public override string QueryString
        {
            get
            {
                ResetParam();
                AddParam("cmd", CmdName);
                AddParam("did", Did);
                AddParam("type", "base64");

                return ParamsToQueryString();
            }
        }
    }
}
