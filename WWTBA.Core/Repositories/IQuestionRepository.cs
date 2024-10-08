using WWTBA.Core.Models;

namespace WWTBA.Core.Repositories
{
    public interface IQuestionRepository : IGenericRepository<Question>
    {
        Task<List<Question>> GetQuestionsWithSubjectAsync();
        public Task<Question> GetQuestionWithAnswersAsync(int questionId);

        public Task<IEnumerable<Question>> GetUnsolvedQuestionsBySubject(int userId, int subjectId);
    }
}

