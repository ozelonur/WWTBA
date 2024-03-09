using WWTBA.Core.Models;

namespace WWTBA.Core.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetUserWithAnswersAsync(int userId);
        Task<User> GetByUsernameAsync(string username);
        Task<User> GetByEmailAsync(string email);
        Task<User> GetUserByEmailVerificationCodeAsync(string verificationCode);
        Task<User> GetUserByVerificationCodeIfValidAsync(string email, string verificationCode, DateTime currentTime);
        Task SetPasswordResetCodeAsync(string email, string resetCode, DateTime createdAt);
        Task<User> GetUserByPasswordResetCodeAsync(string resetCode);
        Task UpdatePasswordAsync(int userId, string newPasswordHash);
    }
}