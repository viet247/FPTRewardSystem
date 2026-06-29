namespace FPTRewardSystem.API.Dtos
{
    public class AuthResponseDto
    {
        public UserResponseDto userResponseDto { get; set; }
        public string Jwt { get; set; }
    }
}
