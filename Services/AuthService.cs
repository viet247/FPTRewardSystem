using FPTRewardSystem.API.Data;
using FPTRewardSystem.API.Dtos;
using FPTRewardSystem.API.Exceptions;
using FPTRewardSystem.API.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FPTRewardSystem.API.Services
{
    public class AuthService : IAuthService
    {

        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration; // 1. Khai báo biến lưu trữ
        // Bơm AppDbContext vào tầng Service thay vì Controller
        // Bơm thêm IConfiguration vào Constructor
        public AuthService(AppDbContext context, IConfiguration configuration)
        {
            this._context = context;
            this._configuration = configuration;
        }
        public async Task<AuthResponseDto> Login(AuthRequestDto authRDto)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == authRDto.Email);
            if (user == null)
            {
                throw new NotFoundException($"Không tìm thấy User có email la: {authRDto.Email}");
            }
            // Kiểm tra Password bằng BCrypt
            bool isValidPassword = BCrypt.Net.BCrypt.Verify(authRDto.PassWord, user.PasswordHash);

            if (!isValidPassword)
            {
                throw new BadRequestException("Mật khẩu không chính xác.");
            }
            string jwtToken = GenerateJwtToken(user);
            return new AuthResponseDto
            {
                UserResponseDto = new UserResponseDto
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Email = user.Email,
                    Role = user.Role
                },
                Jwt = jwtToken
            };
        }

        private string GenerateJwtToken(User user)
        {
            
            // 1. Đọc chuỗi SecretKey từ file appsettings.json bằng đường dẫn phân cấp
            var secretKey = _configuration["Jwt:Key"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            // 2. Chọn thuật toán mã hóa để ký (Ví dụ: HmacSha256)
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // 3. Đóng gói thông tin User vào Claims
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString()) // Rất quan trọng để phân quyền [Authorize(Roles = "...")]
            };

            // 4. Cấu hình các thông tin tổng hợp của Token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1), // Token có giá trị trong 1 ngày
                SigningCredentials = creds
            };

            // 5. Tiến hành tạo và chuyển thành chuỗi String
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token); // Trả về chuỗi JWT hoàn chỉnh dạng: aaa.bbb.ccc
        }
    }
}
