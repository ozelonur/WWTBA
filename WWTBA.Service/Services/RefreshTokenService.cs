using AutoMapper;
using WWTBA.Core.DTOs;
using WWTBA.Core.Models;
using WWTBA.Core.Repositories;
using WWTBA.Core.Services;
using WWTBA.Core.UnitOfWorks;
using WWTBA.Shared.Interfaces;

namespace WWTBA.Service.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        private IMapper _mapper;

        public RefreshTokenService(IRefreshTokenRepository refreshTokenRepository, IUnitOfWork unitOfWork, IMapper mapper, ITokenService tokenService)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<CustomResponseDto<TokenDto>> CreateRefreshTokenAsync(User user)
        {
            RefreshTokenDto refreshToken = new RefreshTokenDto
            {
                UserId = user.Id,
                Token = _tokenService.GenerateRefreshToken(),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
                IsRevoked = false
            };

            RefreshToken token = _mapper.Map<RefreshToken>(refreshToken);
            

            await _refreshTokenRepository.AddAsync(token);
            await _unitOfWork.CommitAsync();

            TokenDto tokenDto = new TokenDto { Token = _tokenService.GenerateToken(new Dictionary<string, string> { { "UserId", user.Id.ToString() } }), RefreshToken = refreshToken.Token };
            return CustomResponseDto<TokenDto>.Success(200, tokenDto);
        }

        public async Task<CustomResponseDto<TokenDto>> RefreshTokenAsync(string token)
        {
            RefreshToken refreshToken = await _refreshTokenRepository.GetByTokenAsync(token);
            if (refreshToken == null || refreshToken.IsRevoked || refreshToken.Expires <= DateTime.UtcNow)
            {
                return CustomResponseDto<TokenDto>.Fail(404, "Invalid refresh token.");
            }

            refreshToken.IsRevoked = true;
            refreshToken.Revoked = DateTime.UtcNow;
            await _unitOfWork.CommitAsync();
            
            string newAccessToken = _tokenService.GenerateToken(new Dictionary<string, string> { { "UserId", refreshToken.UserId.ToString() } });
            string newRefreshToken = _tokenService.GenerateRefreshToken();

            RefreshToken newRefreshTokenEntity = new RefreshToken
            {
                UserId = refreshToken.UserId,
                Token = newRefreshToken,
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
                IsRevoked = false,
                User = refreshToken.User
            };
            await _refreshTokenRepository.AddAsync(newRefreshTokenEntity);
            await _unitOfWork.CommitAsync();

            return CustomResponseDto<TokenDto>.Success(200, new TokenDto { Token = newAccessToken, RefreshToken = newRefreshToken });
        }

        public async Task<CustomResponseDto<bool>> RevokeTokenAsync(string token)
        {
            RefreshToken refreshToken = await _refreshTokenRepository.GetByTokenAsync(token);
            if (refreshToken == null || refreshToken.IsRevoked)
            {
                return CustomResponseDto<bool>.Fail(404, "Invalid refresh token.");
            }

            refreshToken.IsRevoked = true;
            refreshToken.Revoked = DateTime.UtcNow;
            await _unitOfWork.CommitAsync();

            return CustomResponseDto<bool>.Success(200, true);
        }

        public async Task<CustomResponseDto<bool>> RevokeAllTokensAsync(int userId)
        {
            IEnumerable<RefreshToken> refreshTokens = await _refreshTokenRepository.GetByUserIdAsync(userId);
            if (!refreshTokens.Any())
            {
                return CustomResponseDto<bool>.Fail(404, "No refresh tokens found for this user.");
            }

            foreach (var refreshToken in refreshTokens)
            {
                refreshToken.IsRevoked = true;
                refreshToken.Revoked = DateTime.UtcNow;
            }
            await _unitOfWork.CommitAsync();

            return CustomResponseDto<bool>.Success(200, true);
        }
    }
}
