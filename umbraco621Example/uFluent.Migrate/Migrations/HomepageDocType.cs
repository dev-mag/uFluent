using uFluent;
using uFluent.Consts;
using uFluent.Migrate;

namespace umbraco621Example.uFluent.Migrate.Migrations
{
    public class HomepageDocType : IUmbracoMigration
    {
        public void Migrate()
        {
            var homepageTemplate = Template.Create("Homepage", "Homepage");
            homepageTemplate.Save();

            var homepageDocType = DocumentType.Create("Homepage", "Homepage")
                    .SetDefaultTemplate(homepageTemplate)
                    .CreateTab("Content")
                    .AddProperty("title", "Title", DataTypes.TextString, "Content", true, "This is a description for the page title")
                    .Save();
        }
    }
}