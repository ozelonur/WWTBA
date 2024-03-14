using WWTBA.Core.DTOs;
using WWTBA.Core.Models;

namespace WWTBA.Core.Services
{
    public interface IUserService : IService<User, UserDto>
    {
        Task<CustomResponseDto<UserDto>> RegisterAsync(UserCreateDto dto);
        Task<CustomResponseDto<TokenDto>> LoginAsync(LoginDto loginDto);
        Task<CustomResponseDto<UserWithAnswersDto>> GetUserWithAnswersAsync(int userId);
        Task<CustomResponseDto<NoContentDto>> UpdateAsync(UserUpdateDto dto);

        Task<CustomResponseDto<NoContentDto>> SendVerificationEmailAsync(string email);

        Task<CustomResponseDto<bool>> VerifyEmailAsync(string email, string verificationCode,
            string uniqueIdentifier);

        Task<CustomResponseDto<bool>> IsPasswordResetCodeValid(string email, string verificationCode);

        Task<CustomResponseDto<NoContentDto>> SendPasswordResetCodeAsync(string email);

        Task<CustomResponseDto<NoContentDto>> ResetPasswordAsync(string email, string passwordResetCode,
            string newPassword);
    }
}