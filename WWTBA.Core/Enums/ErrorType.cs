namespace WWTBA.Core.Enums
{
    public enum ErrorType
    {
        UsernameAlreadyTaken = 0,
        EmailAlreadyRegistered = 1,
        UserNotFound = 2,
        VerificationFailed = 3,
        VerificationCodeExpired = 4,
        InvalidPasswordResetCode = 5,
        PasswordResetCodeExpired = 6,
        InvalidPassword = 7,
        DeviceNotFoundForUser = 8,
        DeviceNotFound = 9,
        InvalidOrExpiredDeviceVerificationCode = 10,
        InvalidRefreshToken = 11,
        NoRefreshTokenForUser = 12
    }
}

