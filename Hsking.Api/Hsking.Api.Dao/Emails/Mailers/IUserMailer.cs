using System.Threading.Tasks;

namespace Hsking.Api.Dao.Emails.Mailers
{ 
    public interface IUserMailer
    {
            Task ConfirmEmail(string email,string code);
			//Task PasswordReset(string email);
        Task RecoveryPasswordEmail(string email, string code);
    }
}