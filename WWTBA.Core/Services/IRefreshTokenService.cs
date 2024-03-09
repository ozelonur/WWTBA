using WWTBA.Core.DTOs;
using WWTBA.Core.Models;

namespace WWTBA.Core.Services
{
    public interface IRefreshTokenService
    {
        Task<CustomResponseDto<TokenDto>> CreateRefreshTokenAsync(User user);
        Task<CustomResponseDto<TokenDto>> RefreshTokenAsync(string token);
        Task<CustomResponseDto<bool>> RevokeTokenAsync(string token);
        Task<CustomResponseDto<bool>> RevokeAllTokensAsync(int userId);
    }
}

