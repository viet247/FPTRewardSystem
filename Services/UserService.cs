using FPTRewardSystem.API.Data;
using FPTRewardSystem.API.Dtos;
using Microsoft.EntityFrameworkCore;

namespace FPTRewardSystem.API.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        // Bơm AppDbContext vào tầng Service thay vì Controller
        public UserService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<UserResponseDto>> GetAllUsersAsync()
        {
            return await _context.Users
                .Select(u => new UserResponseDto
                {
                    Id = u.Id,
                    Email = u.Email,
                    FullName = u.FullName,
                    Role = u.Role
                })
                .ToListAsync();
        }
    }
}
