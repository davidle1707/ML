using System;

namespace ML.Utils.Payment.Paypal
{
    [Serializable]
    public class ExecutePaymentResponse : PaymentResponse
    {
        public string PaymentId { get; set; }

        public string ReturnInfo { get; set; }

        public string CreatedTime { get; set; }

        public string UpdateTime { get; set; }

        public string State { get; set; }

        public string Intent { get; set; }
    }
    
}
