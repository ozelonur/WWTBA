using WWTBA.Core.DTOs;
using WWTBA.Core.Models;

namespace WWTBA.Core.Repositories
{
    public interface IUserAnswerRepository : IGenericRepository<UserAnswer>
    {
        public Task<UserAverageDto> GetTotalAverageAsync(int userId);
        public Task<UserSubjectAverageDto> GetSubjectSpecificAverageAsync(int userId, int subjectId);
        public Task<UserAnalysisDto> GetUserAnalysisAsync(int userId);
    }
}

