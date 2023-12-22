using System;
using System.IO;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ML.Utils.Payment.Optimal
{
    public class OptimalProcessor
    {
        private readonly OptimalSetting _setting;
        private const string CardPaymentAPI = "cardpayments/v1";
        private const string HostedPaymentAPI = "hosted/v1";

        public OptimalProcessor(OptimalSetting setting)
        {
            _setting = setting;
        }

        public OptimalHostedResponse HostedPayment(OptimalHostedRequest request)
        {
            var merchantRefNum = Guid.NewGuid().ToString();

            dynamic jsonObject = new JObject();
            jsonObject.merchantRefNum = merchantRefNum;
            jsonObject.currencyCode = request.Currency;
            jsonObject.totalAmount = Convert.ToInt32(request.Amount * 100);

            dynamic extended = new JObject();
            extended.key = "authType";
            extended.value = "auth";

            jsonObject.extendedOptions = new JArray(extended);

            dynamic redirect = new JObject();
            redirect.rel = "on_success";
            redirect.returnKeys = new JArray(new object[] { "id" });
            redirect.uri = request.ReturnUrl;
            jsonObject.redirect = new JArray(redirect);

            dynamic billing = new JObject();
            billing.street = request.Address;
            billing.street2 = request.Address2;
            billing.city = request.City;
            billing.state = request.State;
            billing.country = request.Country;
            billing.zip = request.ZipCode;
            billing.phone = request.PhoneNumber;
            jsonObject.billingDetails = billing;

            dynamic addendumMerchantRefNum = new JObject();
            addendumMerchantRefNum.key = "MerchantRefNum";
            addendumMerchantRefNum.value = merchantRefNum;

            dynamic addendumAmount = new JObject();
            addendumAmount.key = "Amount";
            addendumAmount.value = request.Amount;

            dynamic addendumFullName = new JObject();
            addendumFullName.key = "FullName";
            addendumFullName.value = request.FullName;

            dynamic addendumAddress = new JObject();
            addendumAddress.key = "Address";
            addendumAddress.value = request.Address;

            dynamic addendumCity = new JObject();
            addendumCity.key = "City";
            addendumCity.value = request.City;

            dynamic addendumState = new JObject();
            addendumState.key = "State";
            addendumState.value = request.State;

            dynamic addendumZipCode = new JObject();
            addendumZipCode.key = "ZipCode";
            addendumZipCode.value = request.ZipCode;

            dynamic addendumCountry = new JObject();
            addendumCountry.key = "Country";
            addendumCountry.value = request.Country;

            dynamic addendumPhoneNumber = new JObject();
            addendumPhoneNumber.key = "PhoneNumber";
            addendumPhoneNumber.value = request.PhoneNumber;

            jsonObject.addendumData = new JArray(
                addendumMerchantRefNum,
                addendumAmount,
                addendumFullName,
                addendumAddress,
                addendumCity,
                addendumState,
                addendumZipCode,
                addendumCountry,
                addendumPhoneNumber);

            if (!string.IsNullOrEmpty(request.Address2))
            {
                dynamic addendumAddress2 = new JObject();
                addendumAddress2.key = "Address2";
                addendumAddress2.value = request.Address2;
                jsonObject.addendumData.Add(addendumAddress2);
            }

            var url = new Uri(string.Format("{0}/{1}/orders", _setting.ServiceUrl, HostedPaymentAPI));

            var httpResponse = ExcuteRequest(url, HttpMethod.Post, jsonObject);

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var responseText = streamReader.ReadToEnd();
                var dynamicRes = JObject.Parse(responseText) as dynamic;

                if (dynamicRes.error != null)
                {
                    if (dynamicRes.error.fieldErrors != null)
                    {
                        return new OptimalHostedResponse
                        {
                            Success = false,
                            StatusCode = dynamicRes.error.code,
                            StatusMessage = string.Format("{0} ({1})", (dynamicRes.error.fieldErrors[0].error).Value, (dynamicRes.error.fieldErrors[0].field).Value)
                        };
                    }

                    return new OptimalHostedResponse
                    {
                        Success = false,
                        StatusCode = (dynamicRes.error.code).Value,
                        StatusMessage = (dynamicRes.error.message).Value
                    };
                }

                if (dynamicRes.link != null)
                {
                    for (var i = 0; i < dynamicRes.link.Count; i++)
                    {
                        if ((dynamicRes.link[i].rel).Value == "hosted_payment")
                        {
                            return new OptimalHostedResponse
                            {
                                Success = true,
                                ApprovalUrl = (dynamicRes.link[i].uri).Value
                            };
                        }
                    }
                }

                return new OptimalHostedResponse { Success = false };
            }
        }

        public OptimalHostedResponse Settlement(string orderId, string merchantRefNum, decimal amount)
        {
            var url = new Uri(string.Format("{0}/{1}/orders/{2}/settlement", _setting.ServiceUrl, HostedPaymentAPI, orderId));

            dynamic jsonObject = new JObject();
            jsonObject.amount = Convert.ToInt32(amount * 100);
            jsonObject.merchantRefNum = merchantRefNum;

            var httpResponse = ExcuteRequest(url, HttpMethod.Post, jsonObject);

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var responseText = streamReader.ReadToEnd();
                var dynamicRes = JObject.Parse(responseText) as dynamic;

                if (dynamicRes.error != null)
                {
                    if (dynamicRes.error.fieldErrors != null)
                    {
                        return new OptimalHostedResponse
                        {
                            Success = false,
                            StatusCode = dynamicRes.error.code,
                            StatusMessage = string.Format("{0} ({1})", (dynamicRes.error.fieldErrors[0].error).Value, (dynamicRes.error.fieldErrors[0].field).Value)
                        };
                    }

                    return new OptimalHostedResponse
                    {
                        Success = false,
                        StatusCode = (dynamicRes.error.code).Value,
                        StatusMessage = (dynamicRes.error.message).Value
                    };
                }

                if (dynamicRes.confirmationNumber != null)
                {
                    return new OptimalHostedResponse
                    {
                        Success = true,
                        TransactionId = (dynamicRes.confirmationNumber).Value
                    };
                }

                return new OptimalHostedResponse { Success = false };
            }
        }

        public OptimalResponse ProcessPayment(OptimalRequest request)
        {
            dynamic jsonObject = new JObject();

            jsonObject.merchantRefNum = Guid.NewGuid();
            jsonObject.amount = Convert.ToInt32(request.Amount * 100);
            jsonObject.settleWithAuth = true;

            dynamic card = new JObject();
            card.cardNum = request.CardNumber;
            card.cvv = request.CardVerifyNumber;

            dynamic cardExpiry = new JObject();
            cardExpiry.month = request.CardExpirationMonth;
            cardExpiry.year = request.CardExpirationYear;

            card.cardExpiry = cardExpiry;
            jsonObject.card = card;

            dynamic billing = new JObject();
            billing.street = request.Address;
            billing.street2 = request.Address2;
            billing.city = request.City;
            billing.state = request.State;
            billing.country = request.Country;
            billing.zip = request.ZipCode;
            billing.phone = request.PhoneNumber;
            jsonObject.billingDetails = billing;

            var url = new Uri(string.Format("{0}/{1}/accounts/{2}/auths", _setting.ServiceUrl, CardPaymentAPI, _setting.AccountNumber));
            var reponse = ExcuteRequest(url, HttpMethod.Post, jsonObject);

            using (var streamReader = new StreamReader(reponse.GetResponseStream()))
            {
                var responseText = streamReader.ReadToEnd();
                var dynamicRes = JObject.Parse(responseText) as dynamic;

                if (dynamicRes.error != null)
                {
                    if (dynamicRes.error.fieldErrors != null)
                    {
                        return new OptimalResponse
                        {
                            Success = false,
                            StatusCode = dynamicRes.error.code,
                            StatusMessage = string.Format("{0} ({1})", (dynamicRes.error.fieldErrors[0].error).Value, (dynamicRes.error.fieldErrors[0].field).Value)
                        };
                    }

                    return new OptimalResponse
                    {
                        Success = false,
                        StatusCode = (dynamicRes.error.code).Value,
                        StatusMessage = (dynamicRes.error.message).Value
                    };
                }

                if (dynamicRes.status != null && dynamicRes.status == "COMPLETED")
                {
                    return new OptimalResponse
                    {
                        Success = true,
                        TransactionId = (dynamicRes.id).Value,
                        StatusMessage = (dynamicRes.status).Value,
                        CardType = (dynamicRes.card.type).Value

                    };
                }

                return new OptimalResponse { Success = false };
            }
        }

        #region private function

        private string GetAuthString()
        {
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(_setting.ApiKey));
        }

        private HttpWebResponse ExcuteRequest(Uri url, HttpMethod method, JObject json)
        {
            var req = (HttpWebRequest)WebRequest.Create(url);
            req.Headers["Authorization"] = "Basic " + GetAuthString();
            req.ContentType = "application/json; charset=utf-8";
            req.Method = method.Method;

            using (var streamWriter = new StreamWriter(req.GetRequestStream()))
            {
                if (json != null)
                {
                    streamWriter.Write(json.ToString());
                }
            }

            HttpWebResponse httpResponse = null;

            try
            {
                httpResponse = (HttpWebResponse)req.GetResponse();
            }
            catch (WebException ex)
            {
                var response = (HttpWebResponse)ex.Response;
                string exceptionType = null;
                switch (response.StatusCode)
                {
                    case HttpStatusCode.BadRequest: // 400
                        exceptionType = "InvalidRequestException";
                        break;
                    case HttpStatusCode.Unauthorized: // 401
                        exceptionType = "InvalidCredentialsException";
                        break;
                    case HttpStatusCode.PaymentRequired: //402
                        exceptionType = "RequestDeclinedException";
                        break;
                    case HttpStatusCode.Forbidden: //403
                        exceptionType = "PermissionException";
                        break;
                    case HttpStatusCode.NotFound: //404
                        exceptionType = "EntityNotFoundException";
                        break;
                    case HttpStatusCode.Conflict: //409
                        exceptionType = "RequestConflictException";
                        break;
                    case HttpStatusCode.NotAcceptable: //406
                    case HttpStatusCode.UnsupportedMediaType: //415
                    case HttpStatusCode.InternalServerError: //500
                    case HttpStatusCode.NotImplemented: //501
                    case HttpStatusCode.BadGateway: //502
                    case HttpStatusCode.ServiceUnavailable: //503
                    case HttpStatusCode.GatewayTimeout: //504
                    case HttpStatusCode.HttpVersionNotSupported: //505
                        exceptionType = "APIException";
                        break;
                }

                if (exceptionType != null)
                {
                    return response;
                }
            }
            return httpResponse;
        }

        #endregion
    }
}
