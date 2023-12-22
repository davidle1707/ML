using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using ML.Utils.Phone.Vendors.Vitelity.Base;

namespace ML.Utils.Phone.Vendors.Vitelity.Models
{
    public class FaxListMyDidsResponse : BaseResponse
    {
        public List<MyDidsResponse> Data { get; set; }

        internal new void Parse(VitelityResponseXml xmlResponse)
        {
            base.Parse(xmlResponse);
            if (Success)
            {
                var doc = XDocument.Parse(xmlResponse.InnerXml);

                if (doc.Root != null)
                {
                    var sentFaxesElement = doc.Root.Element("response");

                    if (sentFaxesElement != null)
                    {
                        Data = sentFaxesElement.Elements("faxnumber").Select(s => new MyDidsResponse
                        {
                            Did = (string)s.Element("did"),
                            Email = (string)s.Element("email"),
                            RateCenter = (string)s.Element("ratecenter")
                        }).ToList();
                    }
                }
            }
        }
    }

    public class MyDidsResponse
    {
        public string Did { get; set; }

        public string Email { get; set; }

        public string RateCenter { get; set; }
    }
}
