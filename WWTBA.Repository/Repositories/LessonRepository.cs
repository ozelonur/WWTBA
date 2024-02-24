using Microsoft.EntityFrameworkCore;
using WWTBA.Core.Models;
using WWTBA.Core.Repositories;

namespace WWTBA.Repository.Repositories
{
    public class LessonRepository : GenericRepository<Lesson>, ILessonRepository
    {
        public LessonRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Lesson> GetLessonWithQuestionsAsync(int lessonId)
        {
            return await context.Lessons.Include(x => x.Subjects).Where(x => x.Id == lessonId)
                .SingleOrDefaultAsync();
        }
    }
}

