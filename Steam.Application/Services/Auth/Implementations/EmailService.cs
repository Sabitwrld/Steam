using Steam.Application.Services.Auth.Interfaces;
using Steam.Domain.Settings;
using System.Net;
using System.Net.Mail;

namespace Steam.Application.Services.Auth.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly AuthSettings _settings;

        public EmailService(AuthSettings settings)
        {
            _settings = settings;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            using var client = new SmtpClient(_settings.SmtpHost, _settings.SmtpPort)
            {
                Credentials = new NetworkCredential(_settings.SmtpUser, _settings.SmtpPass),
                EnableSsl = _settings.SmtpUseSSL
            };

            var mailMessage = new MailMessage(_settings.SmtpUser, to, subject, body)
            {
                IsBodyHtml = true
            };

            await client.SendMailAsync(mailMessage);
        }
    }
}
