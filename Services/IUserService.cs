using FPTRewardSystem.API.Dtos;
using System.Runtime.InteropServices;

namespace FPTRewardSystem.API.Services
{
    public interface IUserService
    {
        //Trong kiến trúc phần mềm, chúng ta luôn tạo Interface trước để định nghĩa các "đầu việc" cần làm,
        //giúp code linh hoạt và dễ viết Unit Test sau này.
        Task<List<UserResponseDto>> GetAllUsersAsync();
        Task<UserResponseDto> CreateUserAsync(CreateUserRequestDto requestDto);
        Task<UserResponseDto> UpdateUserAsync(Guid id, CreateUserRequestDto requestDto);
        }
}
