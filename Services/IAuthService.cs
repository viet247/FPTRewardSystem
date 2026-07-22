using FPTRewardSystem.API.Dtos;

namespace FPTRewardSystem.API.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto> Login(AuthRequestDto authRDto);
        Task Logout(LogoutRequestDto logoutRequestDto);
    }
}
