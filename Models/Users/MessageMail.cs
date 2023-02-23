using MimeKit;

namespace GuacAPI.Models.Users
{
    public class MessageMail
    {
        public List<MailboxAddress> To{ get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public MessageMail(IEnumerable<string> to, string subject, string content)
        {
            To = new List<MailboxAddress>();
            foreach (var item in to)
            {
                To.AddRange(to.Select(x => new MailboxAddress("email",x)));
            }
            Subject = subject;
            Content = content;
        }
    }
}