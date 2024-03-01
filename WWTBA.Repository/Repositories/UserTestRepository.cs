using WWTBA.Core.Models;
using WWTBA.Core.Repositories;

namespace WWTBA.Repository.Repositories
{
    public class UserTestRepository : GenericRepository<UserTest>, IUserTestRepository
    {
        public UserTestRepository(AppDbContext context) : base(context)
        {
        }
    }
}

