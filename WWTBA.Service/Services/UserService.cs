using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using WWTBA.Core.DTOs;
using WWTBA.Core.Enums;
using WWTBA.Core.GlobalStrings;
using WWTBA.Core.Models;
using WWTBA.Core.Repositories;
using WWTBA.Core.Services;
using WWTBA.Core.UnitOfWorks;
using WWTBA.Shared.Interfaces;

namespace WWTBA.Service.Services
{
    public class UserService : Service<User, UserDto>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IEmailService _emailService;
        private readonly IDeviceService _deviceService;

        public UserService(IGenericRepository<User> repository, IUnitOfWork unitOfWork, IMapper mapper,
            IUserRepository userRepository, ITokenService tokenService,
            IRefreshTokenService refreshTokenService, IEmailService emailService, IDeviceService deviceService) : base(
            repository, unitOfWork,
            mapper)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _refreshTokenService = refreshTokenService;
            _emailService = emailService;
            _deviceService = deviceService;
        }

        public async Task<CustomResponseDto<UserDto>> AddAsync(UserCreateDto dto)
        {
            User newUser = _mapper.Map<User>(dto);
            await _userRepository.AddAsync(newUser);
            await _unitOfWork.CommitAsync();
            UserDto newDto = _mapper.Map<UserDto>(newUser);
            return CustomResponseDto<UserDto>.Success(StatusCodes.Status200OK, newDto);
        }

        public async Task<CustomResponseDto<UserDto>> RegisterAsync(UserCreateDto dto)
        {
            if (await _userRepository.AnyAsync(u => u.Username == dto.Username))
            {
                return CustomResponseDto<UserDto>.Fail(StatusCodes.Status400BadRequest, (int)ErrorType.UsernameAlreadyTaken);
            }

            if (await _userRepository.AnyAsync(u => u.Email == dto.Email))
            {
                return CustomResponseDto<UserDto>.Fail(StatusCodes.Status400BadRequest, (int)ErrorType.EmailAlreadyRegistered);
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            User user = _mapper.Map<User>(dto);
            user.PasswordHash = hashedPassword;
            await _userRepository.AddAsync(user);
            await _unitOfWork.CommitAsync();
            UserDto userDto = _mapper.Map<UserDto>(user);
            return CustomResponseDto<UserDto>.Success(StatusCodes.Status201Created, userDto);
        }

        public async Task<CustomResponseDto<UserWithAnswersDto>> GetUserWithAnswersAsync(int userId)
        {
            User user = await _userRepository.GetUserWithAnswersAsync(userId);
            UserWithAnswersDto userDto = _mapper.Map<UserWithAnswersDto>(user);
            return CustomResponseDto<UserWithAnswersDto>.Success(StatusCodes.Status200OK, userDto);
        }

        public async Task<CustomResponseDto<NoContentDto>> UpdateAsync(UserUpdateDto dto)
        {
            User newUser = _mapper.Map<User>(dto);
            _userRepository.Update(newUser);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<NoContentDto>.Success(StatusCodes.Status200OK);
        }

        public async Task<CustomResponseDto<NoContentDto>> SendVerificationEmailAsync(string email)
        {
            User user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
            {
                return CustomResponseDto<NoContentDto>.Fail(StatusCodes.Status404NotFound, (int)ErrorType.UserNotFound);
            }

            Random random = new Random();
            string verificationCode = random.Next(0, 999999).ToString("D6");

            string verificationCodeHash = BCrypt.Net.BCrypt.HashPassword(verificationCode);
            user.EmailVerificationCode = verificationCodeHash;
            user.EmailVerificationCodeCreatedAt = DateTime.UtcNow;
            user.EmailVerificationCodeValidityDurationInMinutes = 15;
            _userRepository.Update(user);
            await _unitOfWork.CommitAsync();

            string mailSubject = "Mail Adresinizi doğrulayın";
            await _emailService.SendEmailAsync(email, mailSubject, verificationCode, MailType.VerificationCode);

            return CustomResponseDto<NoContentDto>.Success(StatusCodes.Status200OK);
        }


        public async Task<CustomResponseDto<bool>> VerifyEmailAsync(string email, string verificationCode,
            string uniqueIdentifier)
        {
            User user = await _userRepository
                .Where(u => u.Email == email)
                .FirstOrDefaultAsync();

            if (user == null || !BCrypt.Net.BCrypt.Verify(verificationCode, user.EmailVerificationCode))
            {
                return CustomResponseDto<bool>.Fail(StatusCodes.Status400BadRequest, (int)ErrorType.VerificationFailed);
            }

            DateTime codeCreationTime = user.EmailVerificationCodeCreatedAt ?? DateTime.UtcNow;
            TimeSpan timeSinceCodeCreated = DateTime.UtcNow - codeCreationTime;
            if (timeSinceCodeCreated.TotalMinutes > user.EmailVerificationCodeValidityDurationInMinutes)
            {
                return CustomResponseDto<bool>.Fail(StatusCodes.Status400BadRequest, (int)ErrorType.VerificationCodeExpired);
            }

            user.IsEmailVerified = true;
            user.EmailVerificationCode = null;
            user.EmailVerificationCodeCreatedAt = null;
            _userRepository.Update(user);

            DeviceCreateDto deviceCreateDto = new()
            {
                UserId = user.Id,
                DeviceIdentifier = uniqueIdentifier
            };
            await _deviceService.RegisterOrUpdateDeviceAsync(deviceCreateDto);


            await _unitOfWork.CommitAsync();

            return CustomResponseDto<bool>.Success(StatusCodes.Status200OK, user.IsEmailVerified);
        }

        public async Task<CustomResponseDto<NoContentDto>> SendPasswordResetCodeAsync(string email)
        {
            User user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
            {
                return CustomResponseDto<NoContentDto>.Fail(StatusCodes.Status404NotFound, (int)ErrorType.UserNotFound);
            }

            Random random = new Random();
            string resetCode = random.Next(0, 999999).ToString("D6");

            string resetCodeHash = BCrypt.Net.BCrypt.HashPassword(resetCode);
            user.PasswordResetCode = resetCodeHash;
            user.PasswordResetCodeCreatedAt = DateTime.UtcNow;
            user.PasswordResetCodeValidityDurationInMinutes = 15;
            _userRepository.Update(user);
            await _unitOfWork.CommitAsync();
            string subject = "Parola sıfırlama kodunuz";
            await _emailService.SendEmailAsync(email, subject, resetCode, MailType.PasswordResetCode);

            return CustomResponseDto<NoContentDto>.Success(StatusCodes.Status200OK);
        }


        public async Task<CustomResponseDto<NoContentDto>> ResetPasswordAsync(string email, string passwordResetCode,
            string newPassword)
        {
            User user = await _userRepository.GetByEmailAsync(email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(passwordResetCode, user.PasswordResetCode))
            {
                return CustomResponseDto<NoContentDto>.Fail(StatusCodes.Status400BadRequest,
                    (int)ErrorType.InvalidPasswordResetCode);
            }

            DateTime codeCreationTime = user.PasswordResetCodeCreatedAt ?? DateTime.UtcNow;
            TimeSpan timeSinceCodeCreated = DateTime.UtcNow - codeCreationTime;
            if (timeSinceCodeCreated.TotalMinutes > user.PasswordResetCodeValidityDurationInMinutes)
            {
                return CustomResponseDto<NoContentDto>.Fail(StatusCodes.Status400BadRequest,
                    (int)ErrorType.PasswordResetCodeExpired);
            }

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            user.PasswordResetCode = null;
            user.PasswordResetCodeCreatedAt = null;
            _userRepository.Update(user);
            await _unitOfWork.CommitAsync();

            return CustomResponseDto<NoContentDto>.Success(StatusCodes.Status200OK);
        }


        public async Task<CustomResponseDto<TokenDto>> LoginAsync(LoginDto loginDto)
        {
            User user = await _userRepository.GetByUsernameAsync(loginDto.Username);
            if (user == null)
            {
                return CustomResponseDto<TokenDto>.Fail(StatusCodes.Status404NotFound, (int)ErrorType.UserNotFound);
            }

            bool isValidPassword = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash);
            if (!isValidPassword)
            {
                return CustomResponseDto<TokenDto>.Fail(StatusCodes.Status401Unauthorized, (int)ErrorType.InvalidPassword);
            }

            Dictionary<string, string> claims = new Dictionary<string, string>
            {
                { "UserId", user.Id.ToString() },
                { "Username", user.Username }
            };
            string accessToken = _tokenService.GenerateToken(claims);

            CustomResponseDto<TokenDto> refreshToken = await _refreshTokenService.CreateRefreshTokenAsync(user);

            return CustomResponseDto<TokenDto>.Success(StatusCodes.Status200OK,
                new TokenDto
                {
                    Token = accessToken,
                    Expiration = DateTime.UtcNow.AddHours(1),
                    RefreshToken = refreshToken.Data.RefreshToken
                });
        }
    }
}