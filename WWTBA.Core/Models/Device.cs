namespace WWTBA.Core.Models
{
    public class Device : BaseEntity
    {
        public int UserId { get; set; }
        public string DeviceIdentifier { get; set; }
        public DateTime LastLoginDate { get; set; }
        public string VerificationCode { get; set; }
        public DateTime? VerificationCodeSentAt { get; set; }
        public bool IsVerified { get; set; }
    }
}

