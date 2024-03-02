using WWTBA.Core.DTOs;
using WWTBA.Core.Models;

namespace WWTBA.Core.Repositories
{
    public interface IUserTestRepository : IGenericRepository<UserTest>
    {
        public Task<UserTestResultDto> GetTestResultsAsync(int testId);
    }
}

