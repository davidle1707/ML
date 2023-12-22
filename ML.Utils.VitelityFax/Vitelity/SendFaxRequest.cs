using ML.Utils.VitelityFax.Base;

namespace ML.Utils.VitelityFax.Vitelity
{
    public class SendFaxRequest : BaseVitelityRequest
    {
        public SendFaxRequest()
        {
            CmdName = "sendfax";
        }

        /// <summary>
        /// Fax to number. Requred: Yes
        /// </summary>
        public string FaxNumber { get; set; }

        /// <summary>
        /// The number send fax from. Requred: Yes
        /// </summary>
        public string FaxSrc { get; set; }

        /// <summary>
        /// The receivers name you're sending to
        /// </summary>
        public string ReceiversName { get; set; }

        /// <summary>
        /// The name of the first file
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Base64 encoded data of the file you're sending
        /// </summary>
        public string FileBase64Data { get; set; }

        public override string QueryString
        {
            get
            {
                ResetParam();

                AddParam("faxnum", FaxNumber);
                AddParam("faxsrc", FaxSrc);
                AddParam("recname", ReceiversName);
                AddParam("file1", FileName);
                AddParam("data1", FileBase64Data);

                return ParamsToQueryString();
            }
        }
    }
}