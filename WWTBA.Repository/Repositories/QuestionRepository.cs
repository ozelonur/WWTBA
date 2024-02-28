using Microsoft.EntityFrameworkCore;
using WWTBA.Core.Models;
using WWTBA.Core.Repositories;

namespace WWTBA.Repository.Repositories
{
    public class QuestionRepository : GenericRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Question>> GetQuestionsWithSubjectAsync()
        {
            return await context.Questions.Include(x => x.Subject).ToListAsync();
        }

        public async Task<Question> GetQuestionWithAnswersAsync(int questionId)
        {
            return await context.Questions.Include(x => x.Answers).Where(x => x.Id == questionId)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Question>> GetUnsolvedQuestionsBySubject(int userId, int subjectId)
        {
            List<int> solvedQuestionIds = await context.UserAnswers.Where(sq => sq.UserId == userId)
                .Select(sq => sq.QuestionId).ToListAsync();

            List<Question> unsolvedQuestions = await context.Questions
                .Where(q => q.SubjectId == subjectId && !solvedQuestionIds.Contains(q.Id))
                .Include(q => q.Answers)
                .OrderBy(r => Guid.NewGuid())
                .Take(10).ToListAsync();

            return unsolvedQuestions;
        }
    }
}