using Ninject.Modules;
using uFluent.Migrate.Persistence;
using uFluent.Validation;
using Umbraco.Core;
using Umbraco.Core.Services;

namespace uFluent.Migrate.DependencyInjection
{
    class uFluentMigrateModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDatabaseUtil>().To<DatabaseUtil>();

            Bind<IContentTypeService>().ToMethod(context => ApplicationContext.Current.Services.ContentTypeService);
            Bind<IDataTypeService>().ToMethod(context => ApplicationContext.Current.Services.DataTypeService);
            Bind<IFileService>().ToMethod(context => ApplicationContext.Current.Services.FileService);

            Bind<IMigrationProcessor>().To<MigrationProcessor>();

            Bind<IAliasValidator>().To<AliasValidator>();
        }
    }
}
