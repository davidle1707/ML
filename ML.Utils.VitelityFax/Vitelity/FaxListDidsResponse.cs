using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using ML.Utils.VitelityFax.Base;

namespace ML.Utils.VitelityFax.Vitelity
{
    public class FaxListDidsResponse : BaseResponse
    {
        public List<string> Data { get; set; }

        internal new void Parse(VitelityResponseXml xmlResponse)
        {
            base.Parse(xmlResponse);
            if (Success)
            {
                var doc = XDocument.Parse(xmlResponse.InnerXml);

                if (doc.Root != null)
                {
                    var sentFaxesElement = doc.Root.Element("numbers");

                    if (sentFaxesElement != null)
                    {
                        Data = sentFaxesElement.Elements("did").Select(s => s.Value).ToList();
                    }
                }
            }
        }
    }
}
