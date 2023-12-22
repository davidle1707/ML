using System.Collections.Generic;
using System.Xml;

namespace ML.Utils.VitelityFax.Vitelity
{
    public class VitelityResponseXml : XmlDocument
    {
        public VitelityResponseXml(string xml)
        {
            InnerXml = xml;
        }

        public Status Status
        {
            get
            {
                return SelectSingleNode(@"content/status").GetText().ToLower().Equals("ok") ? Status.Ok : Status.Fail;
            }
        }

        public List<string> Errors
        {
            get
            {
                var result = new List<string>();

                if (HasChildNodes)
                {
                    result.Add(SelectSingleNode(@"content/error").GetText());
                }
                return result;
            }
        }

        public bool HasErrors { get { return Errors.Count > 0; } }
    }
}
