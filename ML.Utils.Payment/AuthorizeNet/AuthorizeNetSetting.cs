
namespace ML.Utils.Payment.AuthorizeNet
{
    public class AuthorizeNetSetting
    {
        public string Url { get; set; }

        public string LoginId { get; set; }

        public string TransactionKey { get; set; }

        public bool UseSandbox { get; set; }
    }
}
