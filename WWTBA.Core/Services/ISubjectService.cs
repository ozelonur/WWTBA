using WWTBA.Core.DTOs;
using WWTBA.Core.Models;

namespace WWTBA.Core.Services
{
    public interface ISubjectService : IService<Subject, SubjectDto>
    {
        Task<CustomResponseDto<SubjectDto>> AddAsync(SubjectCreateDto dto);
        Task<CustomResponseDto<SubjectDto>> UpdateAsync(SubjectUpdateDto dto);

        Task<CustomResponseDto<List<SubjectWithLessonDto>>> GetSubjectsWithDto();
        Task<CustomResponseDto<SubjectWithQuestionsDto>> GetSubjectWithQuestionsDto(int subjectId);
        Task<CustomResponseDto<int>> GetQuestionCountOfASubject(int subjectId);

    }
}

