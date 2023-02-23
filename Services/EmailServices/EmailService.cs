using GuacAPI.Models.Users;
using MailKit.Net.Smtp;
using MimeKit;

namespace GuacAPI.Services.EmailServices;

    public class EmailService : IEmailService
    {
        private readonly EmailConfiguration _emailConfig;
        public EmailService(EmailConfiguration emailConfig)
        {
        _emailConfig = emailConfig;
        }

        public void SendEmail(MessageMail message)
        {
            var emailMessage = CreateEmailMessage(message);
            Send(emailMessage);
        }
        private MimeMessage CreateEmailMessage(MessageMail message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("email", _emailConfig.From));
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
                smtp.Connect(_emailConfig.SmtpServer, _emailConfig.Port);
                smtp.Authenticate(_emailConfig.Username, _emailConfig.Password);
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
