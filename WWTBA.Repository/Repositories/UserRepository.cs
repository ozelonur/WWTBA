using Microsoft.EntityFrameworkCore;
using WWTBA.Core.Models;
using WWTBA.Core.Repositories;

namespace WWTBA.Repository.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<User> GetUserWithAnswersAsync(int userId)
        {
            return await context.Users.Include(x => x.UserAnswers).Where(x => x.Id == userId)
                .SingleOrDefaultAsync();
        }
    }
}

