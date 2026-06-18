using FPTRewardSystem.API.Models;

namespace FPTRewardSystem.API.Dtos
{
    public class UserResponseDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public UserRole Role { get; set; }
    }
}
