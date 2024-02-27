using WWTBA.Core.DTOs;
using WWTBA.Core.Models;

namespace WWTBA.Core.Services
{
    public interface IUserAnswerService : IService<UserAnswer, UserAnswerDto>
    {
        Task<CustomResponseDto<UserAnswerDto>> AddAsync(UserAnswerCreateDto dto);
        Task<CustomResponseDto<NoContentDto>> UpdateAsync(UserAnswerUpdateDto dto);
    }
}

