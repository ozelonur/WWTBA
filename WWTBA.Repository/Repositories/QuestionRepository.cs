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
    }
}