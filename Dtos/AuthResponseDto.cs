namespace FPTRewardSystem.API.Dtos
{
    public class AuthResponseDto
    {
        public UserResponseDto UserResponseDto { get; set; }
        public string Jwt { get; set; }
    }
}
