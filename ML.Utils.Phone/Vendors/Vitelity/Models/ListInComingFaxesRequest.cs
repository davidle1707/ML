
using ML.Utils.Phone.Vendors.Vitelity.Base;

namespace ML.Utils.Phone.Vendors.Vitelity.Models
{
    public class ListInComingFaxesRequest : BaseVitelityRequest
    {
        public string ShowPages { get; set; }

        public ListInComingFaxesRequest()
        {
            CmdName = "listincomingfaxes";
            ShowPages = "yes";
        }
        public override string QueryString
        {
            get
            {
                ResetParam();
                AddParam("cmd", CmdName);
                AddParam("showpages", ShowPages);
                AddParam("type", "base64");

                return ParamsToQueryString();
            }
        }
    }
}
