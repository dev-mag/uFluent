using Ninject;
using uFluent.Migrate.DependencyInjection;

namespace uFluent.Migrate
{
    public static class uFluentMigrate 
    {
        public static void Run() 
        {
            Ioc.Initialize(NinjectIocContainer.Create(new StandardKernel(new uFluentMigrateModule())));
            Ioc.Get<IMigrationProcessor>().Run();
        }
    }
}
