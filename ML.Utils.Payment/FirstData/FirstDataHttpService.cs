using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using ML.Common;
using ML.Utils.Payment.FirstDataAPI;
using log4net;

namespace ML.Utils.Payment.FirstData
{
    public class FirstDataHttpService : IFirstDataProcessor
    {
        private readonly ILog _log = LogManager.GetLogger(typeof(FirstDataHttpService));
        private readonly FirstDataSetting _firstDataSetting;

        #region Constants

        const string Method = "POST\n";
        const string Type = "application/xml"; //REST XML
        const string VersionUri = "/transaction/v14";
        
        #endregion

        public FirstDataHttpService(FirstDataSetting firstDataSetting)
        {
            _firstDataSetting = firstDataSetting;
        }

        public TransactionResult ProcessPayment(Transaction request)
        {
            return CallService(request);
        }

        public TransactionResult GetStatus(Transaction request)
        {
            return CallService(request, "/TransactionInfo");
        }

        private TransactionResult CallService(Transaction request, string methodUri = "")
        {
            // set id and password
            request.ExactID = _firstDataSetting.GatewayId;
            request.Password = _firstDataSetting.PassWord;

            string xmlTransactionString = request.SerializeToPureString();

            //SHA1 hash on XML string
            var encoder = new ASCIIEncoding();
            byte[] xmlByte = encoder.GetBytes(xmlTransactionString);
            var sha1Crypto = new SHA1CryptoServiceProvider();
            string hash = BitConverter.ToString(sha1Crypto.ComputeHash(xmlByte)).Replace("-", "");
            string hashedContent = hash.ToLower();

            //assign values to hashing and header variables
            //string keyID = _firstDataSetting.KeyId;//key ID
            //string hmacKey = _firstDataSetting.HmacKey;//Hmac key

            string time = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");

            string hashData = Method + Type + "\n" + hashedContent + "\n" + time + "\n" + VersionUri + methodUri;
            //hmac sha1 hash with key + hash_data
            HMAC hmacSha1 = new HMACSHA1(Encoding.UTF8.GetBytes(_firstDataSetting.HmacKey)); //key
            byte[] hmacData = hmacSha1.ComputeHash(Encoding.UTF8.GetBytes(hashData)); //data
            //base64 encode on hmac_data
            string base64Hash = Convert.ToBase64String(hmacData);
            string url = _firstDataSetting.ApiUrl + methodUri; //Endpoint

            //begin HttpWebRequest 
            var webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Method = "POST";
            webRequest.ContentType = Type;
            webRequest.Accept = "*/*";
            webRequest.Headers.Add("x-gge4-date", time);
            webRequest.Headers.Add("x-gge4-content-sha1", hashedContent);
            webRequest.Headers.Add("Authorization", "GGE4_API " + _firstDataSetting.KeyId + ":" + base64Hash);
            webRequest.ContentLength = xmlTransactionString.Length;

            // write and send request data 
            using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
            {
                streamWriter.Write(xmlTransactionString);
            }

            //get response
            TransactionResult result = null;
            string responseString = String.Empty;
            try
            {
                using (var weResponse = (HttpWebResponse)webRequest.GetResponse())
                {
                    using (var responseStream = new StreamReader(weResponse.GetResponseStream()))
                    {
                        responseString = responseStream.ReadToEnd();
                    }

                    if (_firstDataSetting.Debug)
                    {
                        _log.DebugFormat("Request - HEADER {0}. CONTENT {1}", webRequest.Headers.ToString(),
                                         System.Web.HttpUtility.HtmlEncode(xmlTransactionString));
                        _log.DebugFormat("Response - HEADER {0}. CONTENT: {1}", weResponse.Headers.ToString(),
                                         System.Web.HttpUtility.HtmlEncode(responseString));
                    }

                    result = responseString.Deserialize<TransactionResult>();
                }
            }
            catch (WebException ex)
            {
                //read stream for remote error response
                if (ex.Response != null)
                {
                    using (var errorResponse = (HttpWebResponse)ex.Response)
                    {
                        using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                        {
                            responseString = reader.ReadToEnd();
                            try
                            {
                                result = responseString.Deserialize<TransactionResult>();
                            }
                            catch
                            {
                                result = new TransactionResult
                                {
                                    Transaction_Error = true,
                                    EXact_Message = ex.Message
                                };
                            }
                        }
                    }
                }

                _log.Error(ex);
            }

            return result;
        }
    }
}
