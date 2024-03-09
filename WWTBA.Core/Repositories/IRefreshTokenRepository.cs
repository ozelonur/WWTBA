using WWTBA.Core.Models;

namespace WWTBA.Core.Repositories
{
    public interface IRefreshTokenRepository : IGenericRepository<RefreshToken>
    {
        Task<IEnumerable<RefreshToken>> GetByUserIdAsync(int userId);
        Task<RefreshToken> GetByTokenAsync(string token);
        Task UpdateTokenExpirationAsync(string token, DateTime newExpiration);
        Task DeleteAllByUserIdAsync(int userId);
    }
}

