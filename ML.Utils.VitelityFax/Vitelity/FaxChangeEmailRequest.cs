
using ML.Utils.VitelityFax.Base;

namespace ML.Utils.VitelityFax.Vitelity
{
    public class FaxChangeEmailRequest : BaseVitelityRequest
    {
        public FaxChangeEmailRequest()
        {
            CmdName = "faxchangeemail";
        }

        public string Did { get; set; }

        public string Emails { get; set; }

        public override string QueryString
        {
            get
            {
                ResetParam();
                AddParam("cmd", CmdName);
                AddParam("did", Did);
                AddParam("emails", Emails);
                AddParam("type", "base64");

                return ParamsToQueryString();
            }
        }
    }
}
