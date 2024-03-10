namespace WWTBA.Core.Models
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool IsEmailVerified { get; set; }
        public string EmailVerificationCode { get; set; }
        public DateTime? EmailVerificationCodeCreatedAt { get; set; }
        public int EmailVerificationCodeValidityDurationInMinutes { get; set; } = 15;
        
        public string PasswordResetCode { get; set; }
        public DateTime? PasswordResetCodeCreatedAt { get; set; }
        public int PasswordResetCodeValidityDurationInMinutes { get; set; } = 15;
        
        public string PasswordHash { get; set; }
        public ICollection<UserAnswer> UserAnswers { get; set; }
        public ICollection<UserTest> UserTests { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; }
        public ICollection<Device> Devices { get; set; }
    }
}