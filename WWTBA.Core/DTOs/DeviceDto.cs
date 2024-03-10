namespace WWTBA.Core.DTOs
{
    public class DeviceDto : BaseDto
    {
        public string DeviceIdentifier { get; set; }
        public DateTime LastLoginDate { get; set; }
        public DateTime? VerificationCodeSentAt { get; set; }
        public bool IsVerified { get; set; }
    }
}