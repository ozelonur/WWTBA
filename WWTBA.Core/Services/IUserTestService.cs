using WWTBA.Core.DTOs;
using WWTBA.Core.Models;

namespace WWTBA.Core.Services
{
    public interface IUserTestService : IService<UserTest, UserTestDto>
    {
        Task<CustomResponseDto<UserTestDto>> AddAsync(UserTestCreateDto dto);
        Task<CustomResponseDto<NoContentDto>> UpdateAsync(UserTestUpdateDto dto);

        Task<CustomResponseDto<UserTestResultDto>> GetTestResultsAsync(int testId);
    }
}

