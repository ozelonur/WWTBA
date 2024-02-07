using WWTBA.Core.DTOs;
using WWTBA.Core.Models;

namespace WWTBA.Core.Services
{
    public interface IAnswerService : IService<Answer, AnswerDto>
    {
        Task<CustomResponseDto<AnswerDto>> AddAsync(AnswerCreateDto dto);
        Task<CustomResponseDto<NoContentDto>> UpdateAsync(AnswerUpdateDto dto);
        Task<CustomResponseDto<List<AnswerDto>>> AddRangeAsync(List<AnswerCreateDto> dto);
    }
}

