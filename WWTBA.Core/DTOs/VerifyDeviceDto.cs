namespace WWTBA.Core.DTOs
{
    public class VerifyDeviceDto
    {
        public int UserId { get; set; }
        public string DeviceIdentifier { get; set; }
        public string VerificationCode { get; set; }
    }
}

