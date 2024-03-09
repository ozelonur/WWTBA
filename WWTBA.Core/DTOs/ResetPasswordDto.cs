namespace WWTBA.Core.DTOs
{
    public class ResetPasswordDto
    {
        public string Email { get; set; }
        public string PasswordResetCode { get; set; }
        public string NewPassword { get; set; }
    }
}

