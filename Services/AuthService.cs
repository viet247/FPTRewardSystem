using FPTRewardSystem.API.Data;
using FPTRewardSystem.API.Dtos;
using FPTRewardSystem.API.Exceptions;
using Microsoft.IdentityModel.Tokens;

namespace FPTRewardSystem.API.Services
{
    public class AuthService : IAuthService
    {
        
        private readonly AppDbContext _context;
        // Bơm AppDbContext vào tầng Service thay vì Controller
        public AuthService(AppDbContext context)
        {
            this._context = context;
        }
        public Task<AuthResponseDto> Login(AuthRequestDto authRDto)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == authRDto.Email);
            if(user == null)
            {
                throw new NotFoundException($"Không tìm thấy User có email la: {authRDto.Email}");
            }

        }
    }
}
