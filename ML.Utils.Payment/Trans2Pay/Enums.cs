
namespace ML.Utils.Payment.Trans2Pay
{
    public enum BankAccountType : short
    {
        /// <summary>
        /// Checking
        /// </summary>
        C = 1,
        /// <summary>
        /// Savings
        /// </summary>
        S = 2
    }

    public enum DebitType
    {
        /// <summary>
        /// ACH Monthly Draft   : Draft Type
        /// </summary>
        A,
        /// <summary>
        /// Monthly Draft   : Draft Type
        /// </summary>
        D,
        /// <summary>
        /// Tranfer   : Draft Type
        /// </summary>
        T,
        /// <summary>
        /// Cancellation Fee   : Fee Type
        /// </summary>
        C,
        /// <summary>
        /// Monthly Fee   : Fee Type
        /// </summary>
        M,
        /// <summary>
        /// Retainer Fee   : Fee Type
        /// </summary>
        R,
        /// <summary>
        /// Setup Fee   : Fee Type
        /// </summary>
        S
    }

    public enum PaymentClass
    {
        P,
        W
    }

    public enum PaymentType
    {
        /// <summary>
        /// ACH
        /// </summary>
        A,
        /// <summary>
        /// Manual Check
        /// </summary>
        M,
        /// <summary>
        /// 2nd Day Check
        /// </summary>
        T2,
        /// <summary>
        /// Overnite Check
        /// </summary>
        TO,
        /// <summary>
        /// Wire Transfer
        /// </summary>
        W,
        /// <summary>
        /// Global Direct Pay
        /// </summary>
        X,
        /// <summary>
        /// Settlement Fee
        /// </summary>
        F
    }

    public enum TransactionStatus
    {
        /// <summary>
        /// Cleared
        /// </summary>
        C,
        /// <summary>
        /// Hold
        /// </summary>
        H,
        /// <summary>
        /// NFS
        /// </summary>
        N,
        /// <summary>
        /// Pending
        /// </summary>
        P,
        /// <summary>
        /// Reversed
        /// </summary>
        R,
        /// <summary>
        /// Void
        /// </summary>
        V
    }

    public enum ActiveFlag
    {
        Y,
        N
    }

    public enum T2PAction
    {
        Add,
        Modify,
        Delete
    }
}
