using System.Collections.Generic;

namespace ML.Utils.Payment.PayZang
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
    }
}
