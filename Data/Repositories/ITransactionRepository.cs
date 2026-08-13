using System.Transactions;

namespace FPTRewardSystem.API.Data.Repositories
{
    public interface ITransactionRepository
    {
        Task<Transaction?> GetByIdAsync(Guid id);
        // Lấy danh sách giao dịch phục vụ đối soát trong khoảng thời gian
        Task<IEnumerable<Transaction>> GetReconciliationTransactionsAsync(Guid merchantId, DateTime fromDate, DateTime toDate);
    }
}
