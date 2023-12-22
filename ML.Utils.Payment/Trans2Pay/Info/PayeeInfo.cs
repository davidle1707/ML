using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ML.Utils.Payment.Trans2Pay.Info
{
    public class PayeeInfo
    {
        public string PayeeId { get; set; }

        public string DrcPayeeId { get; set; }

        public string PayeeName{ get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string SocSecNum { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string PhoneNumber { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string ZipCode { get; set; }

        public string BankRoutingNum { get; set; }

        public string BankAccountNum { get; set; }

        public BankAccountType BankAccountType { get; set; }

        public ActiveFlag ActiveFlag { get; set; }
    }

    public class Payee
    {
        

    }
}
