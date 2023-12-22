
namespace ML.Utils.Payment.TransNational
{
    public enum TransactionType
    {
        Sale,
        Auth,
        Capture,
        Void,
        Refund,
        Credit,
        Validate,
        Update
    }

    public enum PaymentType
    {
        Creditcard,
        Check
    }

    public enum AccountHolderType
    {
        Business,
        Personal
    }

    public enum AccountType
    {
        Checking,
        Savings
    }

    public enum StandardEntryClass
    {
        Ppd,
        Web,
        Tel,
        Ccd
    }

    public enum TransactionResponse
    {
        Approved = 1,
        Declined = 2,
        Error=3
    }
}
