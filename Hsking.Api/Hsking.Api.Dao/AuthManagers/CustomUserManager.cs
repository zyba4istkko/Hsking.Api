using System;
using System.Threading.Tasks;
using Hsking.Api.Dto.AuthUsers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;

namespace Hsking.Api.Dao.AuthManagers
{
    public class CustomUserManager : UserManager<ApplicationUser,long>
    {
        public CustomUserManager(IUserStore<ApplicationUser, long> store,IPasswordHasher hasher,DataProtectorTokenProvider<ApplicationUser, long> dataProtectorProvider,IIdentityMessageService smsService)
            : base(store)
        {
            var provider = new DpapiDataProtectionProvider("Sample");
            this.PasswordHasher = hasher;
            this.SmsService = smsService;
            this.UserTokenProvider = dataProtectorProvider;
            (this.UserValidator as UserValidator<ApplicationUser, long>).AllowOnlyAlphanumericUserNames = false;
          
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

        public Task<string> GeneratePassword()
        {
            var generator = new Random();
            var newPassword = generator.Next(0, 1000000).ToString("D6");
            return Task.FromResult(newPassword);
        } 
    }
}
