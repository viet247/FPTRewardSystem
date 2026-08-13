
using FPTRewardSystem.API.Models;

namespace FPTRewardSystem.API.Data.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _context;
        public TransactionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Transaction?> GetByIdAsync(Guid id)
        {
            return await _context.Transactions.FindAsync(id);
        }

        public Task<IEnumerable<Transaction>> GetReconciliationTransactionsAsync(Guid merchantId, DateTime fromDate, DateTime toDate)
        {
            throw new NotImplementedException();
        }
    }
}
