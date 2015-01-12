using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using uFluent;
using uFluent.Consts;
using uFluent.Migrate;
using Umbraco.Core;

namespace uFluentExample.uFluent.Migrate.Migrations
{
    public class HomePageAdditions : IUmbracoMigration
    {
        public void Migrate()
        {
            var homePageDocType = DocumentType.Get("Homepage");
            homePageDocType.AddProperty("intro", "Intro", DataTypes.RichtextEditor, "Content", false)
                .AddProperty("umbracoNaviHide", "Hide from navigation?", DataTypes.TrueFalse, null, false, description: "Hides the page from navigation and sitemaps")
                .SetAllowAtRoot(true)
                .Save();
        }
    }
}