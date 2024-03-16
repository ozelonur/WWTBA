using Microsoft.AspNetCore.Mvc;
using WWTBA.Core.DTOs;
using WWTBA.Core.Services;

namespace WWTBA.API.Controllers
{
    public class DevicesController : CustomBaseController
    {
        private readonly IDeviceService _deviceService;

        public DevicesController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetDevicesByUserId(int userId)
        {
            CustomResponseDto<IEnumerable<DeviceDto>> result = await _deviceService.GetDevicesByUserIdAsync(userId);
            return CreateActionResult(result);
        }

        [HttpPost("verify")]
        public async Task<IActionResult> VerifyDevice(VerifyDeviceDto verifyDeviceDto)
        {
            CustomResponseDto<bool> result = await _deviceService.VerifyDeviceAsync(verifyDeviceDto.UserId,
                verifyDeviceDto.DeviceIdentifier, verifyDeviceDto.VerificationCode);
            return CreateActionResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterOrUpdateDevice(DeviceCreateDto deviceCreateDto)
        {
            CustomResponseDto<DeviceDto> result = await _deviceService.RegisterOrUpdateDeviceAsync(deviceCreateDto);
            return CreateActionResult(result);
        }

        [HttpPost("send-verification-code")]
        public async Task<IActionResult> SendVerificationCodeToDevice(
            SendVerificationCodeToDeviceDto sendVerificationCodeToDeviceDto)
        {
            CustomResponseDto<DeviceDto> result = await _deviceService.CheckAndSendVerificationIfNeededAsync(sendVerificationCodeToDeviceDto.DeviceIdentifier,
                sendVerificationCodeToDeviceDto.Email);
            return CreateActionResult(result);
        }

        [HttpPost("is-device-registered")]
        public async Task<IActionResult> IsDeviceRegistered(DeviceCheckDto dto)
        {
            CustomResponseDto<bool> result = await _deviceService.IsDeviceRegistered(dto);
            return CreateActionResult(result);
        }
    }
}