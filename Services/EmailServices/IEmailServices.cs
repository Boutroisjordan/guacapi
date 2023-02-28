
using GuacAPI.Models.Users;

namespace GuacAPI.Services.EmailServices;

    public interface IEmailService
{
    void SendEmail(MessageMail message);
}
