using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using ML.Utils.VitelityFax.Base;

namespace ML.Utils.VitelityFax.Vitelity
{
    public class ListInComingFaxesResponse : BaseResponse
    {
        public List<CommingFaxesResponse> Data { get; set; }

        internal new void Parse(VitelityResponseXml xmlResponse)
        {
            base.Parse(xmlResponse);
            if (Success)
            {
                var doc = XDocument.Parse(ConvertToWellFormXml(xmlResponse).InnerXml);

                if (doc.Root != null)
                {
                    var faxesElement = doc.Root.Element("faxes");

                    Data = faxesElement.Elements("fax").Select(s => new CommingFaxesResponse
                    {
                        FaxId = (string)s.Element("faxid"),
                        Date = (DateTime)s.Element("date"),
                        FaxSource = (string)s.Element("source"),
                        FaxDestination = (string)s.Element("destination"),
                        Status = (string)s.Element("status"),
                        Pages = (int)s.Element("pages")
                    }).ToList();
                }
            }
        }

        private VitelityResponseXml ConvertToWellFormXml(VitelityResponseXml xmlResponse)
        {
            xmlResponse.InnerXml = xmlResponse.InnerXml.Replace("<faxid>", "<fax><faxid>").Replace("</pages>", "</pages></fax>");

            return xmlResponse;
        }
    }

    public class CommingFaxesResponse
    {
        public string FaxId { get; set; }

        public DateTime Date { get; set; }

        public string FaxSource { get; set; }

        public string FaxDestination { get; set; }

        public string Status { get; set; }

        public int Pages { get; set; } 
    }
}
