using WWTBA.Core.Models;
using WWTBA.Core.Repositories;

namespace WWTBA.Repository.Repositories
{
    public class UserAnswerRepository : GenericRepository<UserAnswer>, IUserAnswerRepository
    {
        public UserAnswerRepository(AppDbContext context) : base(context)
        {
        }
    }
}

