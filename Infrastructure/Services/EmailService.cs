using Domain.Repositories.Services;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;


namespace Infrastructure.Services;



public class EmailService : IEmailService
{
    public async Task SendEmailAsync(string recipient, string subject, string body)
    {
        await SendEmailAsync(new List<string> { recipient }, subject, body);
    }

    public async Task SendEmailAsync(List<string> recipients, string subject, string body)
    {
        Appsettings _mailSettings = Appsettings.Instance;
        var email = new MimeMessage();
        email.Sender = MailboxAddress.Parse(_mailSettings.Email);

        foreach (var recipient in recipients)
            email.To.Add(MailboxAddress.Parse(recipient));

        email.Subject = subject;

        var builder = new BodyBuilder();
        builder.HtmlBody = body;
        email.Body = builder.ToMessageBody();

        using (var smtp = new SmtpClient())
        {
            await smtp.ConnectAsync(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_mailSettings.Email, _mailSettings.Password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
