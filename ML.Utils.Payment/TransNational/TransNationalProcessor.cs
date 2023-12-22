using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Net;
using System.Text;

namespace ML.Utils.Payment.TransNational
{
    public class TransNationalProcessor : BaseClass
    {
        private readonly TransNationalSetting _transNationalSetting;

        public TransNationalProcessor(TransNationalSetting transNationalSetting)
        {
            _transNationalSetting = transNationalSetting;
        }

        private string GetTransNationalUrl()
        {
            //return "https://secure.tnbcigateway.com/api/transact.php";
            return _transNationalSetting.Url;
        }

        public PaymentResponse ProcessPayment(PaymentRequest request)
        {
            var response = new PaymentResponse();
            try
            {
                var webClient = new WebClient();
                var form = new NameValueCollection();
                form.Add("username", _transNationalSetting.UserName);
                form.Add("password", _transNationalSetting.PassWord);
                form.Add("payment", request.PaymentType.ToString());
                form.Add("amount", request.Amount.ToString());

                if (!string.IsNullOrEmpty(request.OrderId))
                    form.Add("orderid", request.OrderId);

                if (!string.IsNullOrEmpty(request.OrderDescription))
                    form.Add("orderdescription", request.OrderDescription);

                if (request.PaymentType == PaymentType.Check)
                {
                    form.Add("checkname", request.NameOnAccount);
                    form.Add("checkaba", request.CustomerBankRoutingNumber);
                    form.Add("checkaccount", request.CustomerBankAccountNumber);
                    form.Add("account_holder_type", request.AccountHolderType.ToString());
                    form.Add("account_type", request.AccountType.ToString());
                    form.Add("sec_code", request.SecCode.ToString());
                    form.Add("currency", request.Currency);
                }
                if (request.PaymentType == PaymentType.Creditcard)
                {
                    form.Add("firstname", request.FirstName);
                    form.Add("lastname", request.LastName);
                    form.Add("address1", request.Address);
                    form.Add("city", request.City);
                    form.Add("state", request.State);
                    form.Add("zip", request.ZipCode);
                    form.Add("type", request.TransactionType.ToString());
                    form.Add("ccnumber", request.CardNumber);
                    form.Add("ccexp", request.CardExpirationDate);
                    form.Add("cvv", request.CardVerifyNumber);
                }

                //Merchant Custom field
                if (!string.IsNullOrEmpty(request.MerchantDefinedField1))
                {
                    form.Add("merchant_defined_field_1", request.MerchantDefinedField1);
                }

                string strResponse = null;
                Byte[] responseData = webClient.UploadValues(GetTransNationalUrl(), form);
                strResponse = Encoding.ASCII.GetString(responseData);

                if (!String.IsNullOrEmpty(strResponse))
                {
                    string[] responseFields = strResponse.Split('&');
                    response.Response = Convert.ToInt32(getValue(responseFields[0].Split('=')[1]));
                    switch (response.Response)
                    {
                        case (int)TransactionResponse.Approved:
                            response.ResponseText = getValue(responseFields[1].Split('=')[1]);
                            response.AuthCode = getValue(responseFields[2].Split('=')[1]);
                            response.TransactionId = getValue(responseFields[3].Split('=')[1]);
                            response.AvsResponse = getValue(responseFields[4].Split('=')[1]);
                            response.CvvResponse = getValue(responseFields[5].Split('=')[1]);
                            response.OrderId = getValue(responseFields[6].Split('=')[1]);
                            response.Type = getValue(responseFields[7].Split('=')[1]);
                            response.ResponseCode = getValue(responseFields[8].Split('=')[1]);
                            break;
                        case (int)TransactionResponse.Declined:
                            response.AddError(getStrError(responseFields[8].Split('=')[1], responseFields[1].Split('=')[1]));
                            break;
                        case (int)TransactionResponse.Error:
                            response.AddError(getStrError(responseFields[8].Split('=')[1], responseFields[1].Split('=')[1]));
                            break;
                    }
                }
                else
                {
                    response.AddError("Cannot connect to payment server!");
                }
            }
            catch (Exception ex)
            {
                response.AddError(ex.ToString());
            }
            return response;
        }
    }
}
