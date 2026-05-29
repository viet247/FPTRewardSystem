namespace FPTRewardSystem.API.Services
{
    public interface ITransactionService
    {
        Task<bool> TransferPointAsync(Guid fromWalletID, Guid toWalletID, decimal amount, string description);
    }
}
