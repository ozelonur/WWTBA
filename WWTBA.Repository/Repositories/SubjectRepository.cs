using Microsoft.EntityFrameworkCore;
using WWTBA.Core.Models;
using WWTBA.Core.Repositories;

namespace WWTBA.Repository.Repositories
{
    public class SubjectRepository : GenericRepository<Subject>, ISubjectRepository
    {
        public SubjectRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Subject>> GetSubjectsWithLessonAsync()
        {
            return await context.Subjects.Include(x => x.Lesson).ToListAsync();
        }

        public async Task<Subject> GetSubjectWithQuestionsAsync(int subjectId)
        {
            return await context.Subjects.Include(x => x.Questions).Where(x => x.Id == subjectId)
                .SingleOrDefaultAsync();
        }
    }
}