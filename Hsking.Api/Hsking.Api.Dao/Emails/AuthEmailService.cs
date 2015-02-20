using System.Threading.Tasks;
using Hsking.Api.Dao.Emails.Mailers;
using Microsoft.AspNet.Identity;

namespace Hsking.Api.Dao.Emails
{
    public class AuthEmailService:IIdentityMessageService
    {
        private readonly IUserMailer _mailer;

        public AuthEmailService(IUserMailer mailer)
        {
            _mailer = mailer;
        }

        public  Task SendAsync(IdentityMessage message)
        {
            switch (message.Subject)
            {
                case "ConfirmEmail":
                     return SentConfirmEmail(message.Body,message.Destination);
                    break;

                case "RecoverPassword":
                    return SentRecover(message.Body, message.Destination);
                    break;

                 default:
                    return Task.FromResult(0);
                    break;
            }
         
        }

        private Task SentRecover(string confirmCode, string email)
        {
            return _mailer.RecoveryPasswordEmail(email, confirmCode);
        }

        private  Task SentConfirmEmail(string confirmCode,string email)
        {
            return _mailer.ConfirmEmail(email, confirmCode);
        }
    }
}
