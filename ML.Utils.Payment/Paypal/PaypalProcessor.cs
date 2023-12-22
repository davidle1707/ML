using System.Linq;
using System.Collections.Generic;
using ppa = PayPal.Api;

namespace ML.Utils.Payment.Paypal
{
    public class PaypalProcessor : BaseClass
    {
        private readonly PaypalSetting _setting;

        public PaypalProcessor(PaypalSetting setting)
        {
            _setting = setting;
        }

        public PaymentResponse ExpressCheckout(PaymentRequest request)
        {
            var response = new PaymentResponse();

            var itemList = new ppa.ItemList
            {
                items = new List<ppa.Item>
                    {
                        new ppa.Item
                        {
                            name = request.Name,
                            currency = request.Currency,
                            price = request.Total.ToString("F"),
                            quantity = "1"
                        }
                    }
            };

            var payer = new ppa.Payer
            {
                payment_method = "paypal",
                //payer_info = new PayerInfo
                //{
                //    billing_address = new Address
                //    {
                //        city = request.City,
                //        country_code = request.CountryCode,
                //        line1 = request.Address,
                //        postal_code = request.ZipCode,
                //        state = request.State,
                //        phone = request.PhoneNumber
                //    }
                //}
            };

            var redirUrls = new ppa.RedirectUrls
            {
                cancel_url = request.CancelUrl,
                return_url = request.ReturnUrl
            };

            var amount = new ppa.Amount
            {
                currency = request.Currency,
                total = request.Total.ToString("F")
            };

            var transactionList = new List<ppa.Transaction>
            {
                new ppa.Transaction
                {
                    description = request.Description,
                    amount = amount,
                    item_list = itemList
                }
            };

            var payment = new ppa.Payment
            {
                intent = TransactMode.Sale.ToString(),
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };

            // Create a payment using a valid APIContext
            GetCreatePaymentResult(payment.Create(GetAPIContext()), ref response);

            return response;
        }

        public PaymentResponse CreateFullSalePayment(PaymentRequest request)
        {
            return CreateFullPayment(request, TransactMode.Sale);
        }

        public PaymentResponse ExcuteExpressCheckout(string paymentId, string payerId)
        {
            var paymentExecution = new ppa.PaymentExecution { payer_id = payerId };
            var payment = new ppa.Payment { id = paymentId };

            // Execute the payment.
            var executedPayment = payment.Execute(GetAPIContext(), paymentExecution);
            if (executedPayment.state.Equals("approved", System.StringComparison.OrdinalIgnoreCase))
            {
                return new PaymentResponse { ReturnId = executedPayment.transactions[0].related_resources[0].sale.id };
            }

            var errorRes = new PaymentResponse();

            errorRes.AddError("Transaction has been " + executedPayment.state);

            foreach (var tran in executedPayment.failed_transactions)
            {
                errorRes.AddError(tran.message);
            }

            return errorRes;
        }

        public ExecutePaymentResponse ExecuteFullPayment(string paymentId, string payerId)
        {
            var paymentExecution = new ppa.PaymentExecution { payer_id = payerId };
            var payment = new ppa.Payment { id = paymentId };

            // Execute the payment.
            var executedResposne = payment.Execute(GetAPIContext(), paymentExecution);

            var response = new ExecutePaymentResponse();

            if (executedResposne.state.Equals("approved", System.StringComparison.OrdinalIgnoreCase))
            {
                response.PaymentId = executedResposne.id;
                response.ReturnId = executedResposne.transactions[0].related_resources[0].sale.id;
                response.CreatedTime = executedResposne.create_time;
                response.UpdateTime = executedResposne.update_time;
                response.State = executedResposne.state;
                response.Intent = executedResposne.intent;

                response.ReturnInfo = Newtonsoft.Json.JsonConvert.SerializeObject(executedResposne);
            }
            else
            {
                response.AddError("Transaction has been " + executedResposne.state);

                foreach (var tran in executedResposne.failed_transactions)
                {
                    response.AddError(tran.message);
                }
            }

            return response;
        }

        public PaymentResponse PayoutCreate(PayoutRequest request)
        {
            var payout = new ppa.Payout
            {
                items = new List<ppa.PayoutItem>
                {
                    new ppa.PayoutItem
                    {
                        recipient_type = ppa.PayoutRecipientType.EMAIL,
                        amount = new ppa.Currency
                        {
                            value = request.Total.ToString(),
                            currency = request.Currency
                        },
                        receiver = request.Email
                    }
                }
            };
            var errorRes = new PaymentResponse();

            var response = payout.Create(GetAPIContext(), true);
            if (response.batch_header.batch_status == "SUCCESS")
            {
                var payoutConfirm = ppa.Payout.Get(GetAPIContext(), response.batch_header.payout_batch_id);

                if (payoutConfirm.items.Any(d => d.error != null))
                {
                    foreach (var item in payoutConfirm.items)
                    {
                        errorRes.AddError(item.error.message);
                    }
                }

                if (!errorRes.Success)
                {
                    return errorRes;
                }

                return new PaymentResponse { ReturnId = response.items[0].transaction_id };
            }


            errorRes.AddError("Transaction has been " + response.batch_header.batch_status);

            foreach (var tran in response.items)
            {
                errorRes.AddError(tran.error.message);
            }

            return errorRes;
        }

        private PaymentResponse CreateFullPayment(PaymentRequest request, TransactMode mode)
        {
            var response = new PaymentResponse();

            var payer = new ppa.Payer
            {
                payment_method = "paypal",
            };

            var redirUrls = new ppa.RedirectUrls
            {
                cancel_url = request.CancelUrl,
                return_url = request.ReturnUrl
            };

            var amount = new ppa.Amount
            {
                currency = request.Currency,
                total = request.Total.ToString("F"),
                details = new ppa.Details
                {
                    subtotal = request.Details.Subtotal.ToString("F"),
                    shipping = request.Details.Shipping.ToString("F"),
                    shipping_discount = request.Details.ShippingDiscount.ToString("F"),
                    //tax = request.Details.Tax.ToString("F"),
                    //insurance = request.Details.Insurance.ToString("F"),
                    //handling_fee = request.Details.HandlingFee.ToString("F"),
                }
            };

            var itemList = new ppa.ItemList
            {
                items = request.Items.Select(i => new ppa.Item
                {
                    name = i.Name,
                    currency = request.Currency,
                    price = i.Price.ToString("F"),
                    quantity = i.Quantity.ToString(),
                    description = i.Description,
                    //tax = i.Tax.ToString("F"),
                    //sku = i.Sku
                }).ToList()
            };

            var transactionList = new List<ppa.Transaction>
            {
                new ppa.Transaction
                {
                    description = request.Description,
                    amount = amount,
                    item_list = itemList
                }
            };

            var payment = new ppa.Payment
            {
                intent = mode.ToString(),
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };

            // Create a payment using a valid APIContext
            GetCreatePaymentResult(payment.Create(GetAPIContext()), ref response);

            return response;
        }

        private void GetCreatePaymentResult(ppa.Payment createdResult, ref PaymentResponse response)
        {
            // Using the `links` provided by the `createdPayment` object, we can give the user the option to redirect to PayPal to approve the payment.
            var links = createdResult.links.GetEnumerator();
            while (links.MoveNext())
            {
                var link = links.Current;
                if (link != null && link.rel.ToLower().Trim().Equals("approval_url"))
                {
                    response.ApprovalUrl = link.href;
                    response.ReturnId = createdResult.id;
                }
            }
        }

        private Dictionary<string, string> GetConfig()
        {
            return new Dictionary<string, string>
                       {
                           //{"mode" ,_setting.IsSandBox ? "security-test-sandbox" : "live"},  // PAYPAL API VERSON: 1.7.2
                           {"mode" ,_setting.IsSandBox ? "sandbox" : "live"}, // PAYPAL API VERSON: 1.7.3 or above
                           {"connectionTimeout" ,"360000"},
                           {"requestRetries" ,"1"},
                           {"clientId" ,_setting.ClientId},
                           {"clientSecret" ,_setting.ClientSecret}
                       };
        }

        private string GetAccessToken()
        {
            try
            {
                var accessToken = new ppa.OAuthTokenCredential(_setting.ClientId, _setting.ClientSecret, GetConfig()).GetAccessToken();
                return accessToken;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

            
        }

        private ppa.APIContext GetAPIContext(string accessToken = "")
        {
            var apiContext = new ppa.APIContext(string.IsNullOrEmpty(accessToken) ? GetAccessToken() : accessToken) { Config = GetConfig() };
            return apiContext;
        }
    }
}
