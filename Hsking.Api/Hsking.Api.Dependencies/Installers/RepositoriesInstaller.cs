using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Hsking.Api.Dao.Repositories;
using Hsking.Api.EfDao;
using Hsking.Api.EfDao.Base;
using Hsking.Api.EfDao.Repositories;


namespace Hsking.Api.Dependencies.Installers
{
    public class RepositoriesInstaller: IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<Hsking_dbEntities3>().LifestyleTransient());

            container.Register(Component.For<IHabitsRepository>().ImplementedBy<HabitsRepository>().LifestyleTransient());
            container.Register(Component.For<IAuthRepository>().ImplementedBy<EfAuthRepository>().LifestyleTransient());
            container.Register(Component.For<IProfileRepository>().ImplementedBy<ProfileRepository>().LifestyleTransient());

        }
    }
}
