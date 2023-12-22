using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML.Utils.Payment.Paypal
{
    public class PayoutRequest
    {
        public PayoutRequest()
        {
            Currency = "USD";
        }

        public string Email { get; set; }

        public decimal Total { get; set; }
            
        public string Currency { get; set; }

    }
}
