using GuacAPI.Models.Users;
using MailKit.Net.Smtp;
using MimeKit;

namespace GuacAPI.Services.EmailServices
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfiguration _emailConfiguration;
        public EmailService(EmailConfiguration emailConfiguration)
        {
            _emailConfiguration = emailConfiguration;
        }

        public void SendEmail(MessageMail message)
        {
            var emailMessage = CreateEmailMessage(message);
            Send(emailMessage);
        }
        private MimeMessage CreateEmailMessage(MessageMail message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("email", _emailConfiguration.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = message.Content;
            emailMessage.Body = builder.ToMessageBody();
            return emailMessage;
        }
        private void Send(MimeMessage mailMessage)
        {
            using var smtp = new SmtpClient();
            try
            {
                smtp.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort);
                smtp.Authenticate(_emailConfiguration.SmtpUsername, _emailConfiguration.SmtpPassword);
                smtp.Send(mailMessage);

            }
            catch
            {
                throw;
            }
            finally
            {
                smtp.Disconnect(true);
                smtp.Dispose();
            }

        }

    }
}