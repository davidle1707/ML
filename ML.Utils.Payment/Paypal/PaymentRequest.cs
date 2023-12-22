
using System;
using System.Collections.Generic;
using PayPal.Api;

namespace ML.Utils.Payment.Paypal
{
    [Serializable]
    public class PaymentRequest
    {
        public PaymentRequest()
        {
            Currency = "USD";
            Details = new PaymentDetail();
            ShippingInfo = new PaymentShippingInfo();
            Items = new List<PaymentItem>();
        }

        public string Name { get; set; }

        public decimal Total { get; set; }
            
        public string Currency { get; set; }

        public string Description { get; set; }

        public string InvoiceNumber { get; set; }

        public string ReturnUrl { get; set; }

        public string CancelUrl { get; set; }

        public PaymentDetail Details { get; set; }

        public PaymentShippingInfo ShippingInfo { get; set; }

        public List<PaymentItem> Items { get; set; }

    }

    [Serializable]
    public class PaymentDetail
    {
        public decimal Subtotal { get; set; }

        public decimal Tax { get; set; }

        public decimal Shipping { get; set; }

        public decimal ShippingDiscount { get; set; }

        public decimal Insurance { get; set; }

        public decimal HandlingFee { get; set; }


    }

    [Serializable]
    public class PaymentItem {

        public string Name { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal Tax { get; set; }

        public string Sku { get; set; }
    }

    [Serializable]
    public class PaymentShippingInfo
    {
        public string RecipientName { get; set; }

        public string Address { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public string Phone { get; set; }
    }
}
