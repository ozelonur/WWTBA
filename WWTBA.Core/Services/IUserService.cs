using WWTBA.Core.DTOs;
using WWTBA.Core.Models;

namespace WWTBA.Core.Services
{
    public interface IUserService : IService<User, UserDto>
    {
        Task<CustomResponseDto<UserDto>> AddAsync(UserCreateDto dto);
        Task<CustomResponseDto<UserWithAnswersDto>> GetUserWithAnswersAsync(int userId);
        Task<CustomResponseDto<NoContentDto>> UpdateAsync(UserUpdateDto dto);
    }
}

