using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using ML.Utils.Phone.Vendors.Vitelity.Base;

namespace ML.Utils.Phone.Vendors.Vitelity.Models
{
    public class SentFaxStatusResponse : BaseResponse
    {
        public SentFaxStatusResponse()
        {
            Data = new List<FaxStatusResponse>();
        }

        public List<FaxStatusResponse> Data { get; set; }

        internal new void Parse(VitelityResponseXml xmlResponse)
        {
            base.Parse(xmlResponse);
            if (Success)
            {
                var doc = XDocument.Parse(xmlResponse.InnerXml);

                if (doc.Root != null)
                {
                    var sentFaxesElement = doc.Root.Element("sentfaxes");

                    if(sentFaxesElement != null)
                    {
                        Data = sentFaxesElement.Elements("fax").Select(s => new FaxStatusResponse
                                                                                {
                                                                                    JobId = (string)s.Element("jobid"),
                                                                                    Date = (DateTime)s.Element("date"),
                                                                                    FaxSource = (string)s.Element("source"),
                                                                                    FaxDestination = (string)s.Element("destination"),
                                                                                    Status = (string)s.Element("status")
                                                                                }).ToList();
                    }
                }
            }
        }
    }

    public class FaxStatusResponse
    {
        public string JobId { get; set; }

        public DateTime Date { get; set; }

        public string FaxSource { get; set; }

        public string FaxDestination { get; set; }

        public string Status { get; set; }
    }
}
