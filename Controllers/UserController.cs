using FluentValidation;
using FPTRewardSystem.API.Data;
using FPTRewardSystem.API.Dtos;
using FPTRewardSystem.API.Exceptions;
using FPTRewardSystem.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FPTRewardSystem.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")] // Duong dan se la: api/v1/user
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IValidator<UserSearchQueryDto> _validator;
        // Constructor: Nơi .NET Core tự động bơm DataContext vào
        // Inject Validator qua Constructor
        public UserController(IUserService userService, IValidator<UserSearchQueryDto> validator)
        {
            _userService = userService;
            _validator = validator;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<UserResponseDto>> Create([FromBody] CreateUserRequestDto requestDto)
        {
            var result = await _userService.CreateUserAsync(requestDto);
            // Trả về HTTP Status Code 201 Created chuẩn RESTful
            return Created($"/api/v1/user/{result.Id}", result);
        }

        [HttpPut("{id}")]// {id} là tham số động trên URL (Route Parameter)
        [Authorize]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserRequestDto requestDto)
        {
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUserRole = User.FindFirst(ClaimTypes.Role)?.Value;
            if (currentUserRole != "Admin" && Guid.Parse(currentUserId) != id)
            {
                throw new ForbiddenException("Ban khong co quyen Update!");
            }
            await _userService.UpdateUserAsync(id, requestDto);
            return NoContent();
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _userService.DeleteUserAsync(id);
            return NoContent();
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] UserSearchQueryDto queryDto)
        {
            var validationResult = await _validator.ValidateAsync(queryDto);
            if (!validationResult.IsValid)
            {
                throw new AppValidationException(validationResult.ToDictionary());
            }
            var result = await _userService.GetUsersAsync(queryDto);
            return Ok(result);
        }
    }
}
