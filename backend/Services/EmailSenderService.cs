using System.Net.Mail;
using System.Net;


namespace wBialy.Services
{
    public interface IEmailSenderService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }

    public class EmailSenderService : IEmailSenderService
    {
        private readonly EmailSenderSettings _settings;
        public EmailSenderService(EmailSenderSettings settings)
        {
            _settings = settings;
        }
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var client = new SmtpClient("smtp-mail.outlook.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(_settings.Email, _settings.Password)
            };
            await client.SendMailAsync(
                new MailMessage(from: _settings.Email,
                to: email,
                subject,
                message));
        }
    }
}
