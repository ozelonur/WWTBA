using WWTBA.Core.DTOs;
using WWTBA.Core.Models;

namespace WWTBA.Core.Services
{
    public interface ILessonService : IService<Lesson, LessonDto>
    {
        Task<CustomResponseDto<LessonDto>> AddAsync(LessonCreateDto dto);
        Task<CustomResponseDto<LessonDto>> UpdateAsync(LessonUpdateDto dto);
    }
}

