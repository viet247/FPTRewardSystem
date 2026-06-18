using FPTRewardSystem.API.Data;
using FPTRewardSystem.API.Dtos;
using FPTRewardSystem.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FPTRewardSystem.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")] // Duong dan se la: api/v1/user
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        // Constructor: Nơi .NET Core tự động bơm DataContext vào
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public async Task<ActionResult<List<UserResponseDto>>> GetAll()
        {
            // Controller giờ chỉ làm nhiệm vụ gọi Service và trả kết quả
            var result = _userService.GetAllUsersAsync();
            return Ok(result);
        }
    }
}
