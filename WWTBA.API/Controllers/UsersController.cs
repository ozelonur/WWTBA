using Microsoft.AspNetCore.Mvc;
using WWTBA.API.Filters;
using WWTBA.Core.DTOs;
using WWTBA.Core.Models;
using WWTBA.Core.Services;

namespace WWTBA.API.Controllers
{
    public class UsersController : CustomBaseController
    {
        private readonly IUserService _userService;
        private readonly IRefreshTokenService _refreshTokenService;

        public UsersController(IUserService userService, IRefreshTokenService refreshTokenService)
        {
            _userService = userService;
            _refreshTokenService = refreshTokenService;
        }

        [ServiceFilter(typeof(NotFoundFilter<User, UserWithAnswersDto>))]
        [HttpGet("[action]/{userId}")]
        public async Task<IActionResult> GetQuestionWithAnswers(int userId)
        {
            return CreateActionResult(await _userService.GetUserWithAnswersAsync(userId));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return CreateActionResult(await _userService.GetAllAsync());
        }

        [ServiceFilter(typeof(NotFoundFilter<User, UserWithAnswersDto>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return CreateActionResult(await _userService.GetByIdAsync(id));
        }

        [ServiceFilter(typeof(NotFoundFilter<User, UserWithAnswersDto>))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            return CreateActionResult(await _userService.RemoveAsync(id));
        }

        [HttpGet("Any/{id}")]
        public async Task<IActionResult> Any(int id)
        {
            return CreateActionResult(await _userService.AnyAsync(x => x.Id == id));
        }

        [HttpGet("[action]/{username}")]
        public async Task<IActionResult> AnyUsername(string username)
        {
            return CreateActionResult(await _userService.AnyAsync(x => x.Username == username));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UserUpdateDto dto)
        {
            return CreateActionResult(await _userService.UpdateAsync(dto));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserCreateDto userCreateDto)
        {
            CustomResponseDto<UserDto> result = await _userService.RegisterAsync(userCreateDto);
            return CreateActionResult(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            CustomResponseDto<TokenDto> result = await _userService.LoginAsync(loginDto);
            return CreateActionResult(result);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto request)
        {
            CustomResponseDto<TokenDto> result = await _refreshTokenService.RefreshTokenAsync(request.RefreshToken);
            return CreateActionResult(result);
        }

        [HttpPost("send-verification-email")]
        public async Task<IActionResult> SendVerificationEmail(SendVerificationEmailDto dto)
        {
            CustomResponseDto<NoContentDto> result = await _userService.SendVerificationEmailAsync(dto.Email);
            return CreateActionResult(result);
        }

        [HttpPost("verify-email")]
        public async Task<IActionResult> VerifyEmail(VerifyEmailDto dto)
        {
            CustomResponseDto<bool> result =
                await _userService.VerifyEmailAsync(dto.Email, dto.VerificationCode, dto.DeviceUniqueIdentifier);
            return CreateActionResult(result);
        }

        [HttpPost("send-password-reset-code")]
        public async Task<IActionResult> SendPasswordResetCode(SendPasswordResetCodeDto dto)
        {
            CustomResponseDto<NoContentDto> result = await _userService.SendPasswordResetCodeAsync(dto.Email);
            return CreateActionResult(result);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto dto)
        {
            CustomResponseDto<NoContentDto> result =
                await _userService.ResetPasswordAsync(dto.Email, dto.PasswordResetCode, dto.NewPassword);
            return CreateActionResult(result);
        }
    }
}