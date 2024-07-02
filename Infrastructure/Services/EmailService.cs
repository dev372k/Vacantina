using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Shared.Helpers;


namespace Infrastructure.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string recipient, string subject, string body);
        Task SendEmailAsync(List<string> recipients, string subject, string body);
    }

    public class EmailService : IEmailService
    {
        public async Task SendEmailAsync(string recipient, string subject, string body)
        {
            await SendEmailAsync(new List<string> { recipient }, subject, body);
        }

        public async Task SendEmailAsync(List<string> recipients, string subject, string body)
        {
            try
            {
                Appsettings _mailSettings = Appsettings.Instance;
                var email = new MimeMessage();
                email.Sender = new MailboxAddress(_mailSettings.Name, _mailSettings.Email);
                //email.Sender = MailboxAddress.Parse(_mailSettings.Email);

                foreach (var recipient in recipients)
                    email.To.Add(MailboxAddress.Parse(recipient));

                email.Subject = subject;

                Dictionary<string, string> dictionary = EmailHelper.General(recipients, body);

                //string emailTemplate = EmailHelper.Template(Templates.GENERAL, dictionary);
                string emailTemplate = string.Empty;

                var builder = new BodyBuilder();
                builder.HtmlBody = emailTemplate;
                email.Body = builder.ToMessageBody();

                using (var smtp = new SmtpClient())
                {
                    await smtp.ConnectAsync(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                    await smtp.AuthenticateAsync(_mailSettings.Email, _mailSettings.Password);
                    await smtp.SendAsync(email);
                    await smtp.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
