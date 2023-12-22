using System.Collections.Generic;
using System;

namespace ML.Utils.Payment.Paypal
{
    [Serializable]
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

        public string ReturnId { get; set; }

        public string ApprovalUrl { get; set; }
    }
}
