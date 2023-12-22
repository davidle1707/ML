using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ML.Utils.Payment.Trans2Pay.Info
{
    public class PaymentInfo
    {
        public string PaymentId { get; set; }

        public string CompanyId { get; set; }

        public string AccountId { get; set; }

        public PaymentClass PaymentClass { get; set; }

        public PaymentType PaymentType { get; set; }

        public DateTime EffectiveDate { get; set; }

        public string PaymentAmount { get; set; }

        public string FeeAmount { get; set; }

        public ActiveFlag ActiveFlag { get; set; }

        public string PayeeId { get; set; }

        public string PayeeAddressId { get; set; }

        public string PayeeClientNum { get; set; }

        public string PayeeBankId { get; set; }

        public string PayeeContactId { get; set; }

        public string PayeeName { get; set; }

        public string Memo { get; set; }
    }
}
