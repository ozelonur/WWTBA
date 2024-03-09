using Microsoft.EntityFrameworkCore;
using WWTBA.Core.Models;
using WWTBA.Core.Repositories;

namespace WWTBA.Repository.Repositories
{
    public class RefreshTokenRepository : GenericRepository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<RefreshToken>> GetByUserIdAsync(int userId)
        {
            return await context.RefreshTokens
                .Where(rt => rt.UserId == userId && !rt.IsRevoked)
                .ToListAsync();
        }

        public async Task<RefreshToken> GetByTokenAsync(string token)
        {
            return await context.RefreshTokens
                .FirstOrDefaultAsync(rt => rt.Token == token && !rt.IsRevoked);
        }

        public async Task UpdateTokenExpirationAsync(string token, DateTime newExpiration)
        {
            RefreshToken refreshToken = await context.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == token);
            if (refreshToken != null)
            {
                refreshToken.Expires = newExpiration;
                context.RefreshTokens.Update(refreshToken);
            }
        }

        public async Task DeleteAllByUserIdAsync(int userId)
        {
            List<RefreshToken> refreshTokens = await context.RefreshTokens
                .Where(rt => rt.UserId == userId)
                .ToListAsync();

            if (refreshTokens.Any())
            {
                context.RefreshTokens.RemoveRange(refreshTokens);
            }
        }
    }
}