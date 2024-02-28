using WWTBA.Core.DTOs;
using WWTBA.Core.Models;

namespace WWTBA.Core.Services
{
    public interface IQuestionService : IService<Question, QuestionDto>
    {
        Task<CustomResponseDto<QuestionDto>> AddAsync(QuestionCreateDto dto);
        Task<CustomResponseDto<IEnumerable<QuestionDto>>> AddRangeAsync(IEnumerable<QuestionCreateDto> dtos);
        Task<CustomResponseDto<NoContentDto>> UpdateAsync(QuestionUpdateDto dto);
        Task<CustomResponseDto<List<QuestionWithSubjectDto>>> GetQuestionsWithSubject();
        Task<CustomResponseDto<QuestionWithAnswersDto>> GetQuestionWithAnswersAsync(int questionId);

        Task<CustomResponseDto<IEnumerable<QuestionWithAnswersDto>>> GetUnsolvedQuestionsWithAnswersBySubjectAsync(
            int userId, int subjectId);

    }
}

