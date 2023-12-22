using System.Collections.Generic;

namespace ML.Utils.Payment.TransNational
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

        public int Response { get; set; }
        
        public string ResponseText { get; set; }
        
        public string ResponseCode { get; set; }
        
        public string AuthCode { get; set; }
        
        public string TransactionId { get; set; }
        
        public string AvsResponse { get; set; }
        
        public string CvvResponse { get; set; }
        
        public string OrderId { get; set; }
        
        public string Type { get; set; }
    }
}
