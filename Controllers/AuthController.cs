using FluentValidation;
using FPTRewardSystem.API.Dtos;
using FPTRewardSystem.API.Exceptions;
using FPTRewardSystem.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FPTRewardSystem.API.Controllers
{
    [ApiController]
    [Route("api/v1/auth")] // Duong dan se la: api/v1/auth
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IValidator<AuthRequestDto> _validator;
        private readonly IValidator<LogoutRequestDto> _logoutValidator;

        // Bơm IAuthService vào để sử dụng logic Login
        // Inject Validator qua Constructor
        public AuthController(IAuthService authService, IValidator<AuthRequestDto> validator, IValidator<LogoutRequestDto> logoutValidator)
        {
            _authService = authService;
            _validator = validator;
            _logoutValidator = logoutValidator;
        }
        [HttpPost("login")] // Đường dẫn sẽ là: api/v1/auth/login
        public async Task<IActionResult> Login([FromBody] AuthRequestDto authRDto)
        {
            // Validate dữ liệu đầu vào
            var validationResult = await _validator.ValidateAsync(authRDto);
            if (!validationResult.IsValid)
            {
                throw new AppValidationException(validationResult.ToDictionary());
            }

            // Gọi xuống tầng Service để xử lý logic và nhận về AuthResponseDto
            var result = await _authService.Login(authRDto);

            // Trả về HTTP Status Code 200 OK kèm theo dữ liệu User + JWT
            return Ok(result);
        }
        [Authorize]
        [HttpPost("logout")] // Đường dẫn sẽ là: api/v1/auth//logout
        public async Task<IActionResult> Logout([FromBody] LogoutRequestDto logoutRequestDto)
        {
            var validationResult = await _logoutValidator.ValidateAsync(logoutRequestDto);
            if (!validationResult.IsValid)
            {
                throw new AppValidationException(validationResult.ToDictionary());
            }
            await _authService.Logout(logoutRequestDto);
            return Ok(new { message = "Đăng xuất thành công" });
        }

    }
}
