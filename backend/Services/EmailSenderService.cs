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
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var mail = "wBialyUserHelper@outlook.com";
            var pw = "userhelperpasswordtowbialy12345!";
            var client = new SmtpClient("smtp-mail.outlook.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(mail, pw)
            };
            await client.SendMailAsync(
                new MailMessage(from: mail,
                to: email,
                subject,
                message));
        }
    }
}
