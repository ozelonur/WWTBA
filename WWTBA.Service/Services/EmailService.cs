using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using WWTBA.Core.GlobalStrings;
using WWTBA.Core.Models;
using WWTBA.Core.Services;

namespace WWTBA.Service.Services
{
    public class EmailService : IEmailService
    {
        private readonly SMTPSettings _smtpSettings;

        public EmailService(IOptions<SMTPSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }

        public async Task<bool> SendEmailAsync(string to, string subject, string verificationCode, MailType mailType)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                string emailTemplatePath = Path.Combine(Directory.GetCurrentDirectory(), "HTML", "EmailTemplate.html");
                string emailTemplate = await File.ReadAllTextAsync(emailTemplatePath);

                string mailBody = mailType switch
                {
                    MailType.VerificationCode => string.Format(emailTemplate, verificationCode,
                        GlobalVariables.verificationCodeHTMLMessage),
                    MailType.PasswordResetCode => string.Format(emailTemplate, verificationCode,
                        GlobalVariables.resetPasswordCodeHTMLMessage),
                    MailType.DeviceVerificationCode => string.Format(emailTemplate, verificationCode,
                        GlobalVariables.verificationCodeDeviceHTMLMessage),
                    _ => throw new ArgumentOutOfRangeException(nameof(mailType), mailType, null)
                };
                using SmtpClient smtpClient = new SmtpClient(_smtpSettings.Server, _smtpSettings.Port);
                smtpClient.Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password);
                smtpClient.EnableSsl = _smtpSettings.EnableSSL;

                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress(_smtpSettings.SenderEmail, _smtpSettings.SenderName),
                    Subject = subject,
                    Body = mailBody,
                    IsBodyHtml = true,
                };
                mailMessage.To.Add(to);

                await smtpClient.SendMailAsync(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
