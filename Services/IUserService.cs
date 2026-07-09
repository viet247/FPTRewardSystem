using FPTRewardSystem.API.Dtos;
using System.Runtime.InteropServices;

namespace FPTRewardSystem.API.Services
{
    public interface IUserService
    {
        //Trong kiến trúc phần mềm, chúng ta luôn tạo Interface trước để định nghĩa các "đầu việc" cần làm,
        //giúp code linh hoạt và dễ viết Unit Test sau này.
        Task<UserResponseDto> CreateUserAsync(CreateUserRequestDto requestDto);
        Task UpdateUserAsync(Guid id, UpdateUserRequestDto requestDto);
        Task DeleteUserAsync(Guid id);

        Task<PagedResult<UserResponseDto>> GetUsersAsync(UserSearchQueryDto queryDto);
        Task<UserResponseDto> GetUserByIdAsync(Guid id);
    }
}
