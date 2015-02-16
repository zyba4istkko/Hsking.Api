using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Hsking.Api.EfDao;
using Hsking.Api.EfDao.Base;
using Hsking.Api.EfDao.Repositories;


namespace Hsking.Api.Dependencies.Installers
{
    public class RepositoriesInstaller: IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<EfContext>().LifestyleTransient());

            container.Register(Component.For<IHabitsRepository>().ImplementedBy<HabitsRepository>().LifestyleTransient());


        }
    }
}
