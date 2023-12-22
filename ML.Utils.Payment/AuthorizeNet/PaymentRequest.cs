
namespace ML.Utils.Payment.AuthorizeNet
{
    public class PaymentRequest
    {
        public TransactMode TransactMode { get; set; }
        
        public string Currency { get; set; }
        
        public decimal Amount { get; set; }

        public string Description{ get; set; }

        public string ExternalId { get; set; }

        //Credit Card
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public string Country { get; set; }

        public string CardNumber{ get; set; }

        public string CardVerifyNumber { get; set; }

        public string CardExpirationDate { get; set; } //format MMYY

    }
}
