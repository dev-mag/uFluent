using uFluent.Migrate;
using Umbraco.Core;

namespace uFluentExample
{
    public class ApplicationStartupHandler : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            base.ApplicationStarted(umbracoApplication, applicationContext);
            uFluentMigrate.Run();
        }
    }


}