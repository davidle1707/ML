
namespace ML.Utils.Payment.TransNational
{
    public class PaymentRequest
    {
        public PaymentType PaymentType { get; set; }
        
        //ACH
        public string NameOnAccount { get; set; }
        
        public string CustomerBankRoutingNumber { get; set; }

        public string CustomerBankAccountNumber { get; set; }

        public AccountHolderType AccountHolderType { get; set; }

        public AccountType AccountType { get; set; }

        public StandardEntryClass SecCode { get; set; }

        public string Currency { get; set; }
        
        public decimal Amount { get; set; }

        //Credit Card
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public TransactionType TransactionType { get; set; }

        public string CardNumber{ get; set; }

        public string CardVerifyNumber { get; set; }

        public string CardExpirationDate { get; set; } //format MMYY

        public string OrderId { get; set; }

        public string OrderDescription { get; set; }

        public string MerchantDefinedField1 { get; set; }
    }
}
