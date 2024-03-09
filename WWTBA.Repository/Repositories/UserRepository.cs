using Microsoft.EntityFrameworkCore;
using WWTBA.Core.Models;
using WWTBA.Core.Repositories;

namespace WWTBA.Repository.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> GetUserWithAnswersAsync(int userId)
        {
            return await _context.Users.Include(x => x.UserAnswers).Where(x => x.Id == userId)
                .SingleOrDefaultAsync();
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetUserByEmailVerificationCodeAsync(string verificationCode)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.EmailVerificationCode == verificationCode);
        }

        public async Task<User> GetUserByVerificationCodeIfValidAsync(string email, string verificationCode,
            DateTime currentTime)
        {
            User user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email && u.EmailVerificationCode == verificationCode);

            if (user == null) return null;
            bool isCodeValid = user.EmailVerificationCodeCreatedAt.HasValue &&
                               user.EmailVerificationCodeCreatedAt.Value.AddMinutes(
                                   user.EmailVerificationCodeValidityDurationInMinutes) > currentTime;

            return isCodeValid ? user : null;
        }

        public async Task SetPasswordResetCodeAsync(string email, string resetCode, DateTime createdAt)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user != null)
            {
                user.PasswordResetCode = resetCode;
                user.PasswordResetCodeCreatedAt = createdAt;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<User> GetUserByPasswordResetCodeAsync(string resetCode)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.PasswordResetCode == resetCode && u.PasswordResetCodeCreatedAt.HasValue
                                                                           && DateTime.UtcNow <=
                                                                           u.PasswordResetCodeCreatedAt.Value
                                                                               .AddMinutes(
                                                                                   u.PasswordResetCodeValidityDurationInMinutes));
        }

        public async Task UpdatePasswordAsync(int userId, string newPasswordHash)
        {
            User user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                user.PasswordHash = newPasswordHash;
                user.PasswordResetCode = null;
                user.PasswordResetCodeCreatedAt = null;
                await _context.SaveChangesAsync();
            }
        }
    }
}