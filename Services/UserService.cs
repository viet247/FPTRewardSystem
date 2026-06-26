using FPTRewardSystem.API.Data;
using FPTRewardSystem.API.Dtos;
using FPTRewardSystem.API.Exceptions;
using FPTRewardSystem.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

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

        public async Task<UserResponseDto> CreateUserAsync(CreateUserRequestDto requestDto)
        {
            var isEmailExist = await _context.Users.AnyAsync(u => u.Email == requestDto.Email);
            if (isEmailExist)
            {
                throw new ConflictException("Email nay da duoc su dung trong he thong");
            }
            // Trước khi ánh xạ, chúng ta tiến hành băm mật khẩu của User gửi lên
            string securedPasswordHash = BCrypt.Net.BCrypt.HashPassword(requestDto.PassWord);
            var newUser = new User
            {
                FullName = requestDto.FullName,
                Email = requestDto.Email,
                PasswordHash = securedPasswordHash,
                Role = UserRole.Employee

            };
            // Đánh dấu thêmp mới vào bộ nhớ
            _context.Users.Add(newUser);
            // Ghi dữ liệu thực tế xuống Database
            await _context.SaveChangesAsync();
            return new UserResponseDto
            {
                Id = newUser.Id,
                Email = newUser.Email,
                FullName = newUser.FullName,
                Role = newUser.Role
            };
        }

        public async Task UpdateUserAsync(Guid id, CreateUserRequestDto requestDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                throw new NotFoundException($"Không tìm thấy User có id: {id}");
            }
            string securedPasswordHash = BCrypt.Net.BCrypt.HashPassword(requestDto.PassWord);
            user.FullName = requestDto.FullName;
            user.Email = requestDto.Email;
            user.PasswordHash = securedPasswordHash;
            await _context.SaveChangesAsync();
        }
    }
}
