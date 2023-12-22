using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ML.Utils.Payment.Trans2Pay.Info
{
    public class DebitInfo
    {
        public string DebitId { get; set; }

        public string AccountId { get; set; }

        public string DrcTransactionId { get; set; }

        public DateTime EffectiveDate { get; set; }

        public decimal Amount { get; set; }

        public string Memo { get; set; }

        public string DebitType { get; set; }

        public string DebitTypeName { get; set; }

        public ActiveFlag ActiveFlag { get; set; }
    }
}
