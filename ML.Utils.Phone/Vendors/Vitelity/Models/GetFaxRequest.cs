using ML.Utils.Phone.Vendors.Vitelity.Base;

namespace ML.Utils.Phone.Vendors.Vitelity.Models
{
    public class GetFaxRequest : BaseVitelityRequest
    {
        public GetFaxRequest()
        {
            CmdName = "getfax";
        }

        /// <summary>
        /// Specific faxid number to capture
        /// </summary>
        public string FaxId { get; set; }

        ///// <summary>
        ///// Download file as base64encoded instead of web download request. Value: base64
        ///// </summary>
        //public string Type { get; set; }

        public override string QueryString
        {
            get
            {
                ResetParam();
                AddParam("cmd", CmdName);
                AddParam("faxid", FaxId);
                AddParam("type", "base64");

                return ParamsToQueryString();
            }
        }
    }
}
