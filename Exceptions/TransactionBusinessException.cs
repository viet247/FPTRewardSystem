namespace FPTRewardSystem.API.Exceptions
{
    public class TransactionBusinessException : Exception
    {
        public TransactionBusinessException(string message) : base(message)
        {
        }
    }
}
