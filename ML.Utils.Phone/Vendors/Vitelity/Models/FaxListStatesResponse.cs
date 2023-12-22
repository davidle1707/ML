
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using ML.Utils.Phone.Vendors.Vitelity.Base;

namespace ML.Utils.Phone.Vendors.Vitelity.Models
{
    public class FaxListStatesResponse : BaseResponse
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
                    var sentFaxesElement = doc.Root.Element("states");

                    if (sentFaxesElement != null)
                    {
                        Data = sentFaxesElement.Elements("state").Select(s => s.Value).ToList();
                    }
                }
            }
        }
    }
}
