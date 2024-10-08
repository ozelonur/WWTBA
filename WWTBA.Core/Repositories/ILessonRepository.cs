using WWTBA.Core.Models;

namespace WWTBA.Core.Repositories
{
    public interface ILessonRepository : IGenericRepository<Lesson>
    {
        Task<Lesson> GetLessonWithQuestionsAsync(int lessonId);
    }
}

