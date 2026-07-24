using FPTRewardSystem.API.Data;
using FPTRewardSystem.API.Dtos;
using FPTRewardSystem.API.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace FPTRewardSystem.API.Services
{
    public class WalletService : IWalletService
    {
        // Bơm AppDbContext vào tầng Service thay vì Controller
        private readonly AppDbContext _dbContext;
    
        public WalletService( AppDbContext dbContext)
        {
            _dbContext = dbContext;

        }
        public async Task<WalletResponseDto> GetWalletByUserIdAsync(string userId)
        {
            if (!Guid.TryParse(userId, out var userGuid))
            {
                throw new BadRequestException("User ID không đúng định dạng GUID");
            }
            var wallet = await _dbContext.Wallets.FirstOrDefaultAsync(w => w.UserId == userGuid);
            if (wallet == null)
            {
                throw new NotFoundException($"Không tìm thấy Wallet");
            }
            return new WalletResponseDto
            {
                Ballance = wallet.Balance
            };
        }
    }
}
