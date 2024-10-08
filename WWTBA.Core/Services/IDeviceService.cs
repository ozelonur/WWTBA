using WWTBA.Core.DTOs;
using WWTBA.Core.Models;

namespace WWTBA.Core.Services
{
    public interface IDeviceService : IService<Device, DeviceDto>
    {
        Task<CustomResponseDto<IEnumerable<DeviceDto>>> GetDevicesByUserIdAsync(int userId);

        Task<CustomResponseDto<DeviceDto>> CheckAndSendVerificationIfNeededAsync(string deviceIdentifier,
            string email);

        Task<CustomResponseDto<bool>> VerifyDeviceAsync(string email, string deviceIdentifier, string verificationCode);
        Task<CustomResponseDto<DeviceDto>> RegisterOrUpdateDeviceAsync(DeviceCreateDto deviceDto);

        Task<CustomResponseDto<bool>> IsDeviceRegistered(DeviceCheckDto deviceDto);
    }
}