using WWTBA.Core.Models;

namespace WWTBA.Core.Repositories
{
    public interface ISubjectRepository : IGenericRepository<Subject>
    {
        Task<List<Subject>> GetSubjectsWithLessonAsync();
    }
}

