using FPTRewardSystem.API.Models;

namespace FPTRewardSystem.API.Dtos
{
    public class CreateUserRequestDto
    {
        public string FullName { get; set; }
        public string PassWord { get; set; }
        public string Email { get; set; }
    }
}
