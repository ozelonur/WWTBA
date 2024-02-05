using WWTBA.Core.DTOs;
using WWTBA.Core.Models;

namespace WWTBA.Core.Services
{
    public interface IQuestionService : IService<Question, QuestionDto>
    {
        Task<CustomResponseDto<QuestionDto>> AddAsync(QuestionCreateDto dto);
        Task<CustomResponseDto<QuestionDto>> UpdateAsync(QuestionUpdateDto dto);
        Task<CustomResponseDto<List<QuestionWithSubjectDto>>> GetQuestionsWithSubject();
        Task<CustomResponseDto<QuestionWithAnswersDto>> GetQuestionWithAnswersAsync(int questionId);

    }
}

