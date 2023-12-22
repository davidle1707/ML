using ML.Utils.Payment.FirstDataAPI;

namespace ML.Utils.Payment.FirstData
{
    public interface IFirstDataProcessor
    {
        TransactionResult ProcessPayment(Transaction request);

        TransactionResult GetStatus(Transaction request);
    }
}
