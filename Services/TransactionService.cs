using FPTRewardSystem.API.Data;
using FPTRewardSystem.API.Models;

namespace FPTRewardSystem.API.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly AppDbContext _context;
        // Tiêm AppDbContext vào để làm việc với Database
        public TransactionService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> TransferPointAsync(Guid fromWalletID, Guid toWalletID, decimal amount, string description)
        {
            // Bước 1: kiểm tra số điểm hợp lệ (phải > 0)
            if (amount <= 0)
                return false;

            // Bước 2: tìm ví người gửi và ví người nhận từ db
            var fromWallet = await _context.Wallets.FindAsync(fromWalletID);
            var toWallet = await _context.Wallets.FindAsync(toWalletID);

            // Bước 3: kiểm tra ví người gửi và ví người nhận có tồn tại không?
            if (fromWallet == null || toWallet == null)
                return false;

            // Bước 4: kiểm tra số dư ví người gửi có đủ chuyển không?
            if (fromWallet.Balance < amount)
                return false;

            // Bước 5: thực hiện chuyển điểm(trừ điểm ví người gửi, cộng điểm ví người nhận)
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Bước 5.1: trừ điểm ví người gửi.
                fromWallet.Balance = fromWallet.Balance - amount;
                // Bước 5.2: cộng điểm ví người nhận
                toWallet.Balance = toWallet.Balance + amount;
                // Bước 5.3: tạo bản ghi lịch sử giao dịch
                var history = new Transaction
                {
                    Id = Guid.NewGuid(),
                    Amount = amount,
                    Description = description,
                    CreatedAt = DateTime.UtcNow,
                    SenderWalletId = fromWalletID,
                    ReceiverWalletId = toWalletID
                };
                _context.Transactions.Add(history);

                // Bước 5.4: đẩy tất cả thay đổi xuống DB cùng lúc
                await _context.SaveChangesAsync();
                // Bước 5.5: xác nhận thành công toàn bộ
                await transaction.CommitAsync();
                return true;
            }
            catch (Exception)
            {
                // Nếu có bất kỳ lỗi nào xảy ra, hủy bỏ toàn bộ thay đổi
                await transaction.RollbackAsync();
                return false;
            }
        }

    }
}
