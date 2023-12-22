using Newtonsoft.Json;

namespace ML.Utils.Payment.Optimal
{
    public class OptimalResponse
    {
        public string StatusCode { get; set; }

        public string StatusMessage { get; set; }

        public string TransactionId { get; set; }

        public string CardType { get; set; }

        public bool Success { get; set; }
    }

    public class OptimalHostedResponse
    {
        public string StatusCode { get; set; }

        public string StatusMessage { get; set; }

        public string TransactionId { get; set; }

        public bool Success { get; set; }

        public string ApprovalUrl { get; set; }
    }
}
