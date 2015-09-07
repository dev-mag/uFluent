using uFluent;
using uFluent.Consts;
using uFluent.Migrate;
using Umbraco.Core;

namespace uFluentExample.uFluent.Migrate.Migrations
{
    public class HomepageCreateTemplateAndDocType : IUmbracoMigration
    {
        public void Migrate()
        {
            var homepageTemplate = Template.Create("Homepage", "Homepage");
            homepageTemplate.Save();

            var homepageDocType = DocumentType.Create("Homepage", "Homepage")
                    .SetDefaultTemplate(homepageTemplate)
                    .CreateTab("Content")
                    .AddProperty("title", "Title", DataTypes.TextString, "Content", true, "This is a description for the page title")
                    .AddProperty("intro", "Intro", DataTypes.RichtextEditor, "Content", false)
                    .AddProperty("umbracoNaviHide", "Hide from navigation?", DataTypes.TrueFalse, null, false, description: "Hides the page from navigation and sitemaps")
                .SetAllowAtRoot(true)
                    .Save();
            
        }
    }
}