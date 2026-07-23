using FPTRewardSystem.API.Dtos;

namespace FPTRewardSystem.API.Services
{
    public interface IWalletService
    {
        public Task<WalletResponseDto> GetWalletByUserIdAsync(string userId);
    }
}
