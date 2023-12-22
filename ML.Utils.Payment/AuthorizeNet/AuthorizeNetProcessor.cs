using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Net;
using System.Text;
using System.Xml.Serialization;
using ANet = AuthorizeNet;
using AuthorizeNet.Api.Controllers;
using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers.Bases;

namespace ML.Utils.Payment.AuthorizeNet
{
    public class AuthorizeNetProcessor : BaseClass
    {
        private readonly AuthorizeNetSetting _setting;

        public AuthorizeNetProcessor(AuthorizeNetSetting setting)
        {
            _setting = setting;
        }

        private string GetAuthorizeUrl()
        {
            return _setting.Url;
        }

        public PaymentResponse ProcessPayment(PaymentRequest request)
        {
            var response = new PaymentResponse();

            var webClient = new WebClient();
            var form = new NameValueCollection();
            form.Add("x_login", _setting.LoginId);
            form.Add("x_tran_key", _setting.TransactionKey);

            form.Add("x_test_request", _setting.UseSandbox ? "TRUE" : "FALSE");

            form.Add("x_delim_data", "TRUE");
            form.Add("x_delim_char", "|");
            form.Add("x_encap_char", "");
            form.Add("x_version", "3.1");
            form.Add("x_relay_response", "FALSE");
            form.Add("x_method", "CC");
            form.Add("x_amount", Math.Round(request.Amount, 2).ToString(CultureInfo.InvariantCulture));
            form.Add("x_currency_code", request.Currency);

            if (request.TransactMode == TransactMode.Authorize)
                form.Add("x_type", "AUTH_ONLY");
            else if (request.TransactMode == TransactMode.AuthorizeAndCapture)
                form.Add("x_type", "AUTH_CAPTURE");
            else
            {
                response.AddError("Not supported transaction mode");
            }

            form.Add("x_card_num", request.CardNumber);
            form.Add("x_exp_date", request.CardExpirationDate);
            form.Add("x_card_code", request.CardVerifyNumber);
            form.Add("x_first_name", request.FirstName);
            form.Add("x_last_name", request.LastName);

            form.Add("x_address", request.Address);
            form.Add("x_city", request.City);
            form.Add("x_state", request.State);
            form.Add("x_zip", request.ZipCode);
            form.Add("x_country", request.Country);
            //x_invoice_num is 20 chars maximum. hece we also pass x_description
            form.Add("x_invoice_num", request.ExternalId);
            form.Add("x_description", request.Description);

            string reply = null;
            Byte[] responseData = webClient.UploadValues(GetAuthorizeUrl(), form);
            reply = Encoding.ASCII.GetString(responseData);

            if (!String.IsNullOrEmpty(reply))
            {
                string[] responseFields = reply.Split('|');
                switch (getValue(responseFields[0]))
                {
                    case "1":
                        response.AuthorizationTransactionCode = string.Format("{0},{1}", getValue(responseFields[6]), getValue(responseFields[4]));
                        response.AuthorizationTransactionResult = string.Format("Approved ({0}: {1})", getValue(responseFields[2]), getValue(responseFields[3]));
                        response.AvsResult = getValue(responseFields[5]);
                        response.TransactionId = getValue(responseFields[6]);
                        break;
                    case "2":
                        response.AddError(getStrError(responseFields[2], responseFields[3]));
                        break;
                    case "3":
                        response.AddError(getStrError(responseFields[2], responseFields[3]));
                        break;
                }
            }
            else
            {
                response.AddError("Authorize.NET unknown error");
            }

            return response;
        }

        public PaymentResponse ProcessPaymentApi(PaymentRequest req)
        {
            var res = new PaymentResponse();

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = _setting.UseSandbox ? ANet.Environment.SANDBOX : ANet.Environment.PRODUCTION;

            // define the merchant information (authentication / transaction id)
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = _setting.LoginId,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = _setting.TransactionKey,
            };

            var creditCard = new creditCardType
            {
                cardNumber = req.CardNumber,
                expirationDate = req.CardExpirationDate
            };

            //standard api call to retrieve response
            var paymentType = new paymentType { Item = creditCard };

            var transactionRequest = new transactionRequestType
            {
                transactionType = (req.TransactMode == TransactMode.Authorize) ? transactionTypeEnum.authOnlyTransaction.ToString() : transactionTypeEnum.authCaptureTransaction.ToString(),   // charge the card
                amount = Convert.ToDecimal(req.Amount),
                payment = paymentType
            };

            var request = new createTransactionRequest { transactionRequest = transactionRequest };

            // instantiate the contoller that will call the service
            var controller = new createTransactionController(request);
            controller.Execute();

            // get the response from the service (errors contained if any)
            var response = controller.GetApiResponse();

            if (response != null)
            {
                if (response.messages.resultCode == messageTypeEnum.Ok)
                {
                    if (response.transactionResponse != null)
                    {
                        res.AuthorizationTransactionCode = response.transactionResponse.authCode;
                        res.AuthorizationTransactionResult = response.transactionResponse.messages[0].description;
                        res.AvsResult = response.transactionResponse.avsResultCode;
                        res.TransactionId = response.transactionResponse.transId;
                    }
                }
                else
                {
                    if (response.transactionResponse != null)
                    {
                        res.AddError(getStrError(response.transactionResponse.errors[0].errorCode, response.transactionResponse.errors[0].errorText));
                    }
                    res.AddError(getStrError(response.messages.message[0].code, response.messages.message[0].text));
                }
            }
            else
            {
                res.AddError("Authorize.NET unknown error");
            }
            return res;
        }

        public StatusResponse GetStatusByTransactionId(string transactionId)
        {
            if (!string.IsNullOrEmpty(transactionId))
            {
                var reportingGateway = new ANet.ReportingGateway(_setting.LoginId, _setting.TransactionKey, _setting.UseSandbox ? ANet.ServiceMode.Test : ANet.ServiceMode.Live);
                var transaction = reportingGateway.GetTransactionDetails(transactionId);
                if (transaction != null)
                {
                    return new StatusResponse
                               {
                                   TransactionId = transaction.TransactionID,
                                   TransactionStatus = transaction.Status
                               };
                }
            }
            return null;
        }
    }

    public class StatusResponse
    {
        public string TransactionId { get; set; }

        public string TransactionStatus { get; set; }
    }
}
