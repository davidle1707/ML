using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Xml;
using System.Xml.Serialization;
using ML.Utils.DocuSign.API;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using log4net;

namespace ML.Utils.DocuSign
{
    public class DocuSignManager
    {
        private readonly ILog _log = LogManager.GetLogger(typeof(DocuSignManager));

        private readonly DocuSignSetting _setting;

        public DocuSignManager(DocuSignSetting setting)
        {
            _setting = setting;

            if (!_setting.IsValid() && _setting.ThrowExceptionIfError)
            {
                throw new DocuSignException("DocuSign setting is invalid");
            }
        }

        public EnvelopeStatus CreateAndSendEnvelope(DocuSignEnvelope envelope, string pdfFileName, byte[] pdfBytes)
        {
            EnvelopeStatus status = null;

            var apiDocument = new Document
                {
                    Name = pdfFileName,
                    PDFBytes = pdfBytes,
                    ID = "1"
                };

            var apiEnvelope = new Envelope
                {
                    AccountId = _setting.AccountId,
                    Subject = envelope.Subject,
                    EmailBlurb = envelope.EmailBlurb,
                    Recipients = envelope.Recipients.Select(r => new Recipient
                        {
                            ID = r.Id.ToString(),
                            Email = r.Email,
                            UserName = r.UserName,
                            Type = r.TypeCode
                        })
                        .ToArray(),
                    Documents = new[] { apiDocument }
                };

            if (envelope.Recipients.Count > 0)
            {
                apiEnvelope.Tabs = (from recipient in envelope.Recipients
                                    from tab in envelope.Tabs
                                    where tab.ApplicantType == recipient.ApplicantType
                                    select new Tab
                                        {
                                            RecipientID = recipient.Id.ToString(),
                                            DocumentID = apiDocument.ID,
                                            Type = tab.TypeCode,
                                            PageNumber = tab.PageNumber.ToString(),
                                            CustomTabHeight = 40,
                                            XPosition = tab.XPosition.ToString(),
                                            YPosition = (tab.YPosition - 40).ToString()
                                        }).ToArray();
            }

            try
            {
                using (var client = CreateApiClient())
                {
                    status = client.CreateAndSendEnvelope(apiEnvelope);
                }
            }
            catch (Exception ex)
            {
                //_log.Error("Error call CreateAndSendEnvelope", ex);

                if (_setting.ThrowExceptionIfError)
                {
                    throw new DocuSignException("Error call CreateAndSendEnvelope", ex);
                }
            }

            return status;
        }

        /// <summary>
        /// Key (EvelopeId) - Value (Pdf)
        /// </summary>
        public Dictionary<string, byte[]> RequestSignedPdfs(List<string> envelopeIds)
        {
            var statuses = new Dictionary<string, byte[]>();

            try
            {
                using (var client = CreateApiClient())
                {
                    foreach (var envelopeId in envelopeIds)
                    {
                        if (!statuses.ContainsKey(envelopeId))
                        {
                            var envelopePdf = client.RequestPDF(envelopeId);
                            statuses.Add(envelopeId, envelopePdf.PDFBytes);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error("Error call RequestPDF", ex);

                if (_setting.ThrowExceptionIfError)
                {
                    throw new DocuSignException("Error call RequestPDF", ex);
                }
            }

            return statuses;
        }

        /// <summary>
        /// Key (EvelopeId) - Value (EnvelopeStatus)
        /// </summary>
        public Dictionary<string, EnvelopeStatus> RequestEnvelopeStatuses(List<string> envelopeIds)
        {
            var statuses = new Dictionary<string, EnvelopeStatus>();

            try
            {
                using (var client = CreateApiClient())
                {
                    foreach (var envelopeId in envelopeIds)
                    {
                        if (!statuses.ContainsKey(envelopeId))
                        {
                            var status = client.RequestStatus(envelopeId);
                            statuses.Add(envelopeId, status);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error("Error call RequestStatus", ex);

                if (_setting.ThrowExceptionIfError)
                {
                    throw new DocuSignException("Error call RequestStatus", ex);
                }
            }

            return statuses;
        }

        public DocuSignEnvelopeInformation ConnectListener(string xmlPosted)
        {
            var serializer = new XmlSerializer(typeof(DocuSignEnvelopeInformation), "http://www.docusign.net/API/3.0");
            var reader = new XmlTextReader(new StringReader(xmlPosted));

            var envelopeInfo = serializer.Deserialize(reader) as DocuSignEnvelopeInformation;

            return envelopeInfo;
        }

        private APIServiceSoapClient CreateApiClient()
        {
            //var remoteAddress = new EndpointAddress(new Uri("http://webhookapp.com/8684215711772845940"));

            //var binding = ResolveBinding();
            //var remoteAddress = new EndpointAddress(new Uri("http://requestb.in/1ab4ohf1"));

            var client = new APIServiceSoapClient();
            
            if (client.ClientCredentials != null)
            {
                client.ClientCredentials.UserName.UserName = string.Format("[{0}]{1}", _setting.IntegratorKey, _setting.Email);
                client.ClientCredentials.UserName.Password = _setting.Password;
            }

            return client;
        }

        public EnvelopeStatus CreateAndSendEnvelopeByRest(DocuSignEnvelope envelope, string pdfFileName, byte[] pdfBytes)
        {
            string url = "https://demo.docusign.net/restapi/v2/login_information";
            string baseURL = "";    // we will retrieve this
            string accountId = "";  // will retrieve

            var objectCredentials = new { Username = _setting.Email, Password = _setting.Password, IntegratorKey = _setting.IntegratorKey };

            string jSONCredentialsString = JsonConvert.SerializeObject(objectCredentials);

            // 
            // STEP 1 - Login
            //
            try
            {
                //Login
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Headers.Add("X-DocuSign-Authentication", jSONCredentialsString);
                request.Accept = "application/json";
                request.Method = "GET";

                var webResponse = (HttpWebResponse)request.GetResponse();
                var sr = new StreamReader(webResponse.GetResponseStream());
                var responseText = sr.ReadToEnd();
                sr.Close();

                var jObject = JObject.Parse(responseText);
                var jUserAccount = jObject["loginAccounts"].First;

                accountId = (string)jUserAccount["accountId"];
                baseURL = (string)jUserAccount["baseUrl"];

                //Send Envelop
                string formDataBoundary = String.Format("{0:N}", Guid.NewGuid());

                request = (HttpWebRequest)WebRequest.Create(baseURL + "/envelopes");
                //request = (HttpWebRequest)WebRequest.Create("http://webhookapp.com/8684215711772845940");
                //request = (HttpWebRequest)WebRequest.Create("http://requestb.in/" + requestBinParam);
                request.Headers.Add("X-DocuSign-Authentication", jSONCredentialsString);
                request.ContentType = "multipart/form-data; boundary=" + formDataBoundary;
                request.Accept = "application/json";
                request.Method = "POST";
                request.KeepAlive = true;
                request.Credentials = CredentialCache.DefaultCredentials;

                var streamBufferData = new MemoryStream();

                //write envelopes json
                string head = string.Format("\r\n\r\n--{0}\r\nContent-Type: application/json\r\nContent-Disposition: form-data\r\n", formDataBoundary);
                var envelopBody = head + JsonConvert.SerializeObject(GetEnvelopeJson(envelope, pdfFileName));

                var envelopBodyBytes = Encoding.UTF8.GetBytes(envelopBody);
                streamBufferData.Write(envelopBodyBytes, 0, envelopBodyBytes.Length);

                //write document file
                head = string.Format("\r\n\r\n--{0}\r\nContent-Disposition: file; filename=\"{1}\"; documentid={2}\r\n\r\n", formDataBoundary, pdfFileName, 1);
                var docHeaderBytes = Encoding.UTF8.GetBytes(head);
                streamBufferData.Write(docHeaderBytes, 0, docHeaderBytes.Length);
                streamBufferData.Write(pdfBytes, 0, pdfBytes.Length);

                //write end
                var endBytes = Encoding.UTF8.GetBytes(string.Format("\r\n\r\n--{0}--", formDataBoundary));
                streamBufferData.Write(endBytes, 0, endBytes.Length);

                var requestData = streamBufferData.ToArray();
                streamBufferData.Close();

                request.ContentLength = requestData.Length;

                var requestStream = request.GetRequestStream();
                requestStream.Write(requestData, 0, requestData.Length);

                streamBufferData.Close();

                // read the response
                webResponse = (HttpWebResponse)request.GetResponse();
                sr = new StreamReader(webResponse.GetResponseStream());

                responseText = sr.ReadToEnd();

                // display results
                Console.WriteLine("Response of Action Create Envelope with Two Documents --> \r\n " + responseText);
                Console.ReadLine();
            }
            catch (WebException e)
            {
                using (WebResponse response = e.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    Console.WriteLine("Error code: {0}", httpResponse.StatusCode);
                    using (Stream data = response.GetResponseStream())
                    {
                        string text = new StreamReader(data).ReadToEnd();
                        Console.WriteLine(text);
                    }
                }
                Console.ReadLine();
            }

            return null;
        }

        private static object GetEnvelopeJson(DocuSignEnvelope envelope, string pdfFileName)
        {
            Func<IEnumerable<DocuSignTab>, object> tabsToJsonArray = tabs => tabs.Select(t => new
            {
                documentId = 1,
                pageNumber = t.PageNumber,
                xPosition = t.XPosition,
                yPosition = t.YPosition - 40,
                customTabHeight = 40
            }
            ).ToArray();

            Func<DocuSignApplicantType, object> getTabsJson = type =>
            {
                var signHereTabs = envelope.Tabs.Where(t => t.ApplicantType == type && t.TypeCode == TabTypeCode.SignHere);
                var initialHereTabs = envelope.Tabs.Where(t => t.ApplicantType == type && t.TypeCode == TabTypeCode.InitialHere);

                if (signHereTabs.Any() && initialHereTabs.Any())
                {
                    return new
                    {
                        signHereTabs = tabsToJsonArray(signHereTabs),
                        initialHereTabs = tabsToJsonArray(initialHereTabs)
                    };
                }

                if (signHereTabs.Any())
                {
                    return new { signHereTabs = tabsToJsonArray(signHereTabs) };
                }


                if (initialHereTabs.Any())
                {
                    return new { initialHereTabs = tabsToJsonArray(initialHereTabs) };
                }

                return new object[] { };
            };

            var envelopeJson = new
            {
                emailBlurb = envelope.EmailBlurb,
                emailSubject = envelope.Subject,
                status = "sent",
                documents = new[] { new { name = pdfFileName, documentId = 1 } },
                recipients = new
                {
                    signers = envelope.Recipients.Select(r => new
                    {
                        recipientId = r.Id,
                        routingOrder = r.Id,
                        email = r.Email,
                        name = r.UserName,
                        tabs = getTabsJson(r.ApplicantType)
                    }).ToArray()
                }
            };

            return envelopeJson;
        }
    }
}
