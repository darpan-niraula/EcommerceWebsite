using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

public class EmailSender : IEmailSender
{
    private readonly EmailSettings _emailSettings;

    public EmailSender(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    public async Task SendEmailAsync(string email, string subject, string message)
    {
        try
        {
            var client = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.SmtpPort)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_emailSettings.SmtpUsername, _emailSettings.SmtpPassword),
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailSettings.FromEmail, _emailSettings.FromName),
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };
            mailMessage.To.Add(email);

            client.Send(mailMessage);
        }
        catch (Exception ex)
        {
            throw new Exception(message = ex.Message);
        }
    }
}
