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

namespace WWTBA.Service.Services
{
    public class DeviceService : Service<Device, DeviceDto>, IDeviceService
    {
        private readonly IUserRepository _userRepository;
        private IDeviceRepository _deviceRepository;
        private readonly IEmailService _emailService;

        public DeviceService(IGenericRepository<Device> repository, IUnitOfWork unitOfWork, IMapper mapper,
            IUserRepository userRepository, IEmailService emailService, IDeviceRepository deviceRepository) : base(
            repository, unitOfWork, mapper)
        {
            _userRepository = userRepository;
            _emailService = emailService;
            _deviceRepository = deviceRepository;
        }

        public async Task<CustomResponseDto<IEnumerable<DeviceDto>>> GetDevicesByUserIdAsync(int userId)
        {
            IEnumerable<Device> devices = await _deviceRepository.GetDevicesByUserIdAsync(userId);
            if (devices == null || !devices.Any())
            {
                return CustomResponseDto<IEnumerable<DeviceDto>>.Fail(StatusCodes.Status404NotFound,
                    (int)ErrorType.DeviceNotFoundForUser);
            }

            IEnumerable<DeviceDto> deviceDtos = _mapper.Map<IEnumerable<DeviceDto>>(devices);
            return CustomResponseDto<IEnumerable<DeviceDto>>.Success(StatusCodes.Status200OK, deviceDtos);
        }

        public async Task<CustomResponseDto<DeviceDto>> CheckAndSendVerificationIfNeededAsync(string deviceIdentifier, string email)
        {
            User user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
            {
                return CustomResponseDto<DeviceDto>.Fail(StatusCodes.Status404NotFound, (int)ErrorType.UserNotFound);
            }

            Device device = await _deviceRepository.GetDeviceByUserIdAndIdentifierAsync(user.Id, deviceIdentifier);
            if (device == null || !device.IsVerified)
            {
                Random random = new Random();
                string verificationCode = random.Next(0, 999999).ToString("D6");
                string verificationCodeHash = BCrypt.Net.BCrypt.HashPassword(verificationCode);
                
                if (device == null)
                {
                    device = new Device
                    {
                        UserId = user.Id,
                        DeviceIdentifier = deviceIdentifier,
                        LastLoginDate = DateTime.UtcNow,
                        VerificationCode = verificationCodeHash,
                        VerificationCodeSentAt = DateTime.UtcNow,
                        IsVerified = false
                    };
                    await _deviceRepository.AddAsync(device);
                }
                else
                {
                    device.VerificationCode = verificationCodeHash;
                    device.VerificationCodeSentAt = DateTime.UtcNow;
                    _deviceRepository.Update(device);
                }
                await _unitOfWork.CommitAsync();
                
                await _emailService.SendEmailAsync(email, "Cihaz Doğrulama Kodu", verificationCode, MailType.DeviceVerificationCode);

                return CustomResponseDto<DeviceDto>.Success(StatusCodes.Status200OK);
            }

            return CustomResponseDto<DeviceDto>.Success(StatusCodes.Status200OK);
        }

        public async Task<CustomResponseDto<bool>> VerifyDeviceAsync(string email, string deviceIdentifier, string verificationCode)
        {
            User user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
            {
                return CustomResponseDto<bool>.Fail(StatusCodes.Status404NotFound, (int)ErrorType.UserNotFound);
            }
            
            Device device = await _deviceRepository.GetDeviceByUserIdAndIdentifierAsync(user.Id, deviceIdentifier);
            if (device == null)
            {
                return CustomResponseDto<bool>.Fail(StatusCodes.Status404NotFound, (int)ErrorType.DeviceNotFound);
            }

            bool isValidVCode = BCrypt.Net.BCrypt.Verify(verificationCode, device.VerificationCode);

            if (!isValidVCode ||
                !device.VerificationCodeSentAt.HasValue ||
                !((DateTime.UtcNow - device.VerificationCodeSentAt.Value).TotalMinutes <= 5))
                return CustomResponseDto<bool>.Fail(StatusCodes.Status400BadRequest,
                    (int)ErrorType.InvalidOrExpiredDeviceVerificationCode);
            
            device.IsVerified = true;
            device.VerificationCodeSentAt = null;
            device.VerificationCode = null;
            _deviceRepository.Update(device);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<bool>.Success(StatusCodes.Status200OK, true);

        }

        public async Task<CustomResponseDto<DeviceDto>> RegisterOrUpdateDeviceAsync(DeviceCreateDto deviceDto)
        {
            Device device = _mapper.Map<Device>(deviceDto);
            device.IsVerified = true;
            device.LastLoginDate = DateTime.UtcNow;
            await _deviceRepository.AddAsync(device);
            await _unitOfWork.CommitAsync();

            DeviceDto dto = _mapper.Map<DeviceDto>(device);

            return CustomResponseDto<DeviceDto>.Success(StatusCodes.Status201Created, dto);
        }

        public async Task<CustomResponseDto<bool>> IsDeviceRegistered(DeviceCheckDto deviceDto)
        {
            User user = await _userRepository.GetByEmailAsync(deviceDto.Email);
            if (user == null)
            {
                return CustomResponseDto<bool>.Fail(StatusCodes.Status404NotFound, (int)ErrorType.UserNotFound);
            }
            IEnumerable<Device> devices = await _deviceRepository.GetDevicesByUserIdAsync(user.Id);
            if (devices == null || !devices.Any() || devices.All(x => x.DeviceIdentifier != deviceDto.DeviceIdentifier))
            {
                return CustomResponseDto<bool>.Fail(StatusCodes.Status404NotFound,
                    (int)ErrorType.DeviceNotFoundForUser);
            }

            Device device = await _deviceRepository.Where(x => x.DeviceIdentifier == deviceDto.DeviceIdentifier)
                .FirstOrDefaultAsync();
            return CustomResponseDto<bool>.Success(StatusCodes.Status200OK, device.IsVerified);
        }
    }
}