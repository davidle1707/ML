
using ML.Utils.Phone.Vendors.Vitelity.Base;

namespace ML.Utils.Phone.Vendors.Vitelity.Models
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
