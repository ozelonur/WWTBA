using WWTBA.Core.GlobalStrings;

namespace WWTBA.Core.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body, MailType mailType);
    }
}

