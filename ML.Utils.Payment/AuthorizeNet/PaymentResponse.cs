using System.Collections.Generic;

namespace ML.Utils.Payment.AuthorizeNet
{
    public class PaymentResponse
    {
        public IList<string> Errors { get; set; }

        public PaymentResponse() 
        {
            Errors = new List<string>();
        }

        public bool Success
        {
            get { return (Errors.Count == 0); }
        }

        public void AddError(string error)
        {
            Errors.Add(error);
        }

        public string TransactionId { get; set; }

        public string AvsResult { get; set; }

        public string AuthorizationTransactionId { get; set; }

        public string AuthorizationTransactionCode { get; set; }

        public string AuthorizationTransactionResult { get; set; }

        public string CaptureTransactionId { get; set; }

        public string CaptureTransactionResult { get; set; }

        public string SubscriptionTransactionId { get; set; }
        
        public bool AllowStoringCreditCardNumber { get; set; }
    }
}
