using FPTRewardSystem.API.Data;
using FPTRewardSystem.API.Dtos;
using FPTRewardSystem.API.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;
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
            var wallet = await _dbContext.Wallets.FirstOrDefaultAsync(w => w.UserId == Guid.Parse(userId));
            if (wallet == null)
            {
                throw new NotFoundException($"Không tìm thấy Wallet");
            }
            var walletResponseDto = new WalletResponseDto
            {
                Ballance = wallet.Balance
            };
            return walletResponseDto;
        }
    }
}
