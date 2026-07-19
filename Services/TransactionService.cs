using FPTRewardSystem.API.Data;
using FPTRewardSystem.API.Dtos;
using FPTRewardSystem.API.Exceptions;
using FPTRewardSystem.API.Models;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

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
        public async Task<TransactionResponseDto> TransferPointAsync(Guid senderID, TransactionRequestDto requestDto)
        {
            var user = await _context.Users.Include(u => u.Wallet).FirstOrDefaultAsync(u => u.Id == senderID);
            if (user == null)
            {
                throw new NotFoundException($"Không tìm thấy User có id: {senderID}");
            }
            if (user.Wallet == null)
            {
                throw new NotFoundException($"Không tìm thấy Wallet với User có id: {senderID}");
            }
            // Bước 1: tìm ví người gửi và ví người nhận từ db
            var fromWallet = user.Wallet;
            var toWallet = await _context.Wallets.FindAsync(requestDto.ToWalletID);

            // Bước 2: kiểm tra ví người gửi và ví người nhận có tồn tại không?
            if (fromWallet == null || toWallet == null)
            {
                throw new BadRequestException("Thông tin người gửi hoặc người nhận không hợp lệ!");
            }

            // Bước 3: kiểm tra xem người nhận có phải người gửi không?
            if (fromWallet == toWallet)
            {
                throw new TransactionBusinessException("Người nhận phải khác người gửi.");
            }


            // Bước 4: kiểm tra số dư ví người gửi có đủ chuyển không?
            if (fromWallet.Balance < requestDto.Amount)
            {
                throw new TransactionBusinessException("Số dư của bạn không đủ để thực hiện giao dịch này!");
            }

            // Bước 5: thực hiện chuyển điểm(trừ điểm ví người gửi, cộng điểm ví người nhận)
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Bước 5.1: trừ điểm ví người gửi.
                fromWallet.Balance = fromWallet.Balance - requestDto.Amount;
                // Bước 5.2: cộng điểm ví người nhận
                toWallet.Balance = toWallet.Balance + requestDto.Amount;
                // Bước 5.3: tạo bản ghi lịch sử giao dịch
                var history = new Transaction
                {
                    Id = Guid.NewGuid(),
                    Amount = requestDto.Amount,
                    Description = requestDto.Description,
                    CreatedAt = DateTime.UtcNow,
                    SenderWalletId = fromWallet.Id,
                    ReceiverWalletId = toWallet.Id
                };
                _context.Transactions.Add(history);

                // Bước 5.4: đẩy tất cả thay đổi xuống DB cùng lúc
                await _context.SaveChangesAsync();
                // Bước 5.5: xác nhận thành công toàn bộ
                await transaction.CommitAsync();
                return new TransactionResponseDto
                {
                    ID = history.Id,
                    ToWalletID = history.ReceiverWalletId,
                    Description = history.Description,
                    ExistingBalance = fromWallet.Balance
                };
            }
            catch (Exception e)
            {
                // Nếu có bất kỳ lỗi nào xảy ra, hủy bỏ toàn bộ thay đổi
                await transaction.RollbackAsync();
                throw;
            }
        }

    }
}
