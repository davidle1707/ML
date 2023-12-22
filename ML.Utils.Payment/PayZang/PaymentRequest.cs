
using System;
using System.Collections.Generic;

namespace ML.Utils.Payment.PayZang
{
    public class PaymentRequest
    {
        public PaymentRequest()
        {
            PaymentItems = new List<PaymentItem>();
        }

        public List<PaymentItem> PaymentItems { get; set; }
    }

    public class PaymentItem
    {
        public string ContactName { get; set; }

        public string Address { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Payee { get; set; }

        public DateTime Date { get; set; }

        public string CheckNumber { get; set; }

        public decimal Amount { get; set; }

        public string RoutingNumber { get; set; }

        public string AccountNumber { get; set; }

        public string Notes { get; set; }

        public string Memo { get; set; }
    }
}
