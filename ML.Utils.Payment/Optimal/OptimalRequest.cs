namespace ML.Utils.Payment.Optimal
{
    public class OptimalRequest
    {
        public string Currency { get; set; }

        public decimal Amount { get; set; }

        //Credit Card
        public string FullName { get; set; }

        public string Address { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public string Country { get; set; }

        public string PhoneNumber { get; set; }

        public string CardNumber { get; set; }

        public string CardVerifyNumber { get; set; }

        public string CardExpirationMonth { get; set; }

        public string CardExpirationYear { get; set; }
    }

    public class OptimalHostedRequest 
    {
        public string Currency { get; set; }

        public decimal Amount { get; set; }

        //Billing
        public string FullName { get; set; }

        public string Address { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public string Country { get; set; }

        public string PhoneNumber { get; set; }

        public string ReturnUrl { get; set; }
    }
}
