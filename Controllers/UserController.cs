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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        // Constructor: Nơi .NET Core tự động bơm DataContext vào
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<List<UserResponseDto>>> GetAll()
        {
            // Controller giờ chỉ làm nhiệm vụ gọi Service và trả kết quả
            var result = await _userService.GetAllUsersAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<UserResponseDto>> Create([FromBody] CreateUserRequestDto requestDto)
        {
            var result = await _userService.CreateUserAsync(requestDto);
            // Trả về HTTP Status Code 201 Created chuẩn RESTful
            return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]// {id} là tham số động trên URL (Route Parameter)
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserRequestDto requestDto)
        {
            await _userService.UpdateUserAsync(id, requestDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _userService.DeleteUserAsync(id);
            return NoContent();
        }
    }
}
