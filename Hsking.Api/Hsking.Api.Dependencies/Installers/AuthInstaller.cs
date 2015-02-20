using System;
using System.Web.Security;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Hsking.Api.Controllers;
using Hsking.Api.Dao.AuthManagers;
using Hsking.Api.Dao.Emails;
using Hsking.Api.Dao.Emails.Mailers;
using Hsking.Api.Dto.AuthUsers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.OAuth;

namespace Hsking.Api.Dependencies.Installers
{
    public class AuthInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var provider = new MachineKeyProtectionProvider();

            IDataProtector protector = provider.Create("ResetPasswordPurpose");
            container.Register(Component.For<IDataProtector>().Instance(protector));
            container.Register(
                Component.For<DataProtectorTokenProvider<ApplicationUser, long>>().LifestyleTransient());

            container.Register(Component.For<IIdentityMessageService>().ImplementedBy<AuthEmailService>().LifestyleTransient());
            container.Register(Component.For<IPasswordHasher>().ImplementedBy<PasswordHasher>().LifestyleTransient());
            container.Register(Component.For<IUserMailer>().ImplementedBy<UserMailer>().LifestyleTransient());

            container.Register(
              Component.For<IUserStore<ApplicationUser, long>>().ImplementedBy<CustomUserStore>().LifestyleTransient());

            container.Register(Component.For<CustomUserManager>().LifestyleTransient());
            


            container.Register(
                Component.For<OAuthAuthorizationServerOptions>()
                    .UsingFactoryMethod((kernel, parameters) => new OAuthAuthorizationServerOptions
                    {
                        AllowInsecureHttp = true,
                        TokenEndpointPath = new PathString("/api/token"),
                        AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                        Provider = new SimpleAuthorizationServerProvider(container)
                    })
                    .LifestyleTransient());




        }
    }

    public class MachineKeyProtectionProvider : IDataProtectionProvider
    {
        public IDataProtector Create(params string[] purposes)
        {
            return new MachineKeyDataProtector(purposes);
        }
    }

    public class MachineKeyDataProtector : IDataProtector
    {
        private readonly string[] _purposes;

        public MachineKeyDataProtector(string[] purposes)
        {
            _purposes = purposes;
        }

        public byte[] Protect(byte[] userData)
        {
            return MachineKey.Protect(userData, _purposes);
        }

        public byte[] Unprotect(byte[] protectedData)
        {
            return MachineKey.Unprotect(protectedData, _purposes);
        }
    }
}
