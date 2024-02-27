using WWTBA.Core.Models;

namespace WWTBA.Core.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public Task<User> GetUserWithAnswersAsync(int userId);
    }
}

