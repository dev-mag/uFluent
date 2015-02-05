using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using uFluent.Migrate;
using Umbraco.Core;

namespace umbraco621Example
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