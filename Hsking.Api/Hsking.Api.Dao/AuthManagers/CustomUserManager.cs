using System.Threading.Tasks;
using Hsking.Api.Dto.AuthUsers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;

namespace Hsking.Api.Dao.AuthManagers
{
    public class CustomUserManager : UserManager<ApplicationUser,long>
    {
        public CustomUserManager(IUserStore<ApplicationUser, long> store,IPasswordHasher hasher,IIdentityMessageService emailService,DataProtectorTokenProvider<ApplicationUser, long> dataProtectorProvider)
            : base(store)
        {
            var provider = new DpapiDataProtectionProvider("Sample");
            this.PasswordHasher = hasher;
            this.EmailService = emailService;
            this.UserTokenProvider = dataProtectorProvider;
        }

        public override Task<ApplicationUser> FindAsync(string userName, string password)
        {
            Task<ApplicationUser> taskInvoke = Task<ApplicationUser>.Factory.StartNew(() =>
            {
                PasswordVerificationResult result = this.PasswordHasher.VerifyHashedPassword(userName, password);
                if (result == PasswordVerificationResult.SuccessRehashNeeded)
                {
                    return Store.FindByNameAsync(userName).Result;
                }
                return null;
            });
            return taskInvoke;
        }

        
    }
}
