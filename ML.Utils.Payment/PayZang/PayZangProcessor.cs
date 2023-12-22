using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using Renci.SshNet;

namespace ML.Utils.Payment.PayZang
{
    public class PayZangProcessor : BaseClass
    {
        private readonly PayZangSetting _payZangSetting;

        public PayZangProcessor(PayZangSetting payZangSetting)
        {
            _payZangSetting = payZangSetting;
        }

        public PaymentResponse ProcessPayment(PaymentRequest request)
        {
            var response = new PaymentResponse();
            try
            {
                var sftp = new SftpClient(_payZangSetting.FtpUrl, 22, _payZangSetting.FtpUserName, _payZangSetting.FtpPassWord);
                sftp.Connect();
                byte[] buffer = GenerateExcelFile(request);
                using (Stream stream = new MemoryStream(buffer))
                {
                    sftp.UploadFile(stream, _payZangSetting.FtpFileName);
                }

                sftp.Disconnect();

                //var ftprequest = (FtpWebRequest)WebRequest.Create(new Uri(string.Format(@"ftp://{0}/{1}", "127.0.0.1", _payZangSetting.FtpFileName)));
                //ftprequest.Method = WebRequestMethods.Ftp.UploadFile;
                //ftprequest.UseBinary = true;
                //ftprequest.UsePassive = true;
                //ftprequest.KeepAlive = true;
                //ftprequest.Credentials = new NetworkCredential("kiet", "kiet1234");
                //byte[] buffer = GenerateExcelFile(request);
                //Stream requestStream = ftprequest.GetRequestStream();
                //requestStream.Write(buffer, 0, buffer.Length);
                //requestStream.Close();
                //requestStream.Flush();
            }
            catch (Exception ex)
            {
                response.AddError(ex.ToString());
            }
            return response;
        }

        //private function
        private byte[] GenerateExcelFile(PaymentRequest request)
        {
            var csv = new StringBuilder();
            csv.Append("CompanyName/ContactName"); csv.Append(","); //Required
            csv.Append("Address"); csv.Append(","); //Required
            csv.Append("Address2"); csv.Append(",");
            csv.Append("City"); csv.Append(",");//Required
            csv.Append("State"); csv.Append(",");//Required
            csv.Append("PostalCode"); csv.Append(",");//Required
            csv.Append("Phone"); csv.Append(",");
            csv.Append("Email"); csv.Append(",");
            csv.Append("Payee"); csv.Append(",");
            csv.Append("Date"); csv.Append(",");//Required
            csv.Append("CheckNumber"); csv.Append(",");
            csv.Append("Amount"); csv.Append(",");//Required
            csv.Append("RoutingNumber"); csv.Append(",");//Required
            csv.Append("AccountNumber"); csv.Append(",");//Required
            csv.Append("Notes"); csv.Append(",");
            csv.Append("Memo");
            csv.Append(Environment.NewLine);

            foreach (var payment in request.PaymentItems)
            {
                csv.Append(payment.ContactName); csv.Append(","); //Required
                csv.Append(payment.Address); csv.Append(","); //Required
                csv.Append(payment.Address2); csv.Append(",");
                csv.Append(payment.City); csv.Append(",");//Required
                csv.Append(payment.State); csv.Append(",");//Required
                csv.Append(payment.PostalCode); csv.Append(",");//Required
                csv.Append(payment.Phone); csv.Append(",");
                csv.Append(payment.Email); csv.Append(",");
                csv.Append(payment.Payee); csv.Append(",");
                csv.Append(payment.Date.ToString("MM/dd/yyyy")); csv.Append(",");//Required
                csv.Append(payment.CheckNumber); csv.Append(",");
                csv.Append(Math.Round(payment.Amount, 2)); csv.Append(",");//Required
                csv.Append(payment.RoutingNumber); csv.Append(",");//Required
                csv.Append(payment.AccountNumber); csv.Append(",");//Required
                csv.Append(payment.Notes); csv.Append(",");
                csv.Append(payment.Memo);
                csv.Append(Environment.NewLine);
            }

            byte[] buffer = Encoding.UTF8.GetBytes(csv.ToString());
            return buffer;
        }
    }
}
