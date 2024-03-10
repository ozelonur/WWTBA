namespace WWTBA.Core.DTOs
{
    public class SendVerificationCodeToDeviceDto
    {
        public int UserId { get; set; }
        public string DeviceIdentifier { get; set; }
        public string Email { get; set; }
    }
}

