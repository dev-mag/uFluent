using uFluent;
using uFluent.Consts;
using uFluent.Migrate;

namespace umbraco621Example.uFluent.Migrate.Migrations
{
    public class ContentDocTypeWithPRTagProperty : IUmbracoMigration
    {
        public void Migrate()
        {
            var template = Template.Create("ContentPage", "Content Page");
            template.Save();

            var homepage = DocumentType.Get("Homepage");

            var dt = DocumentType.Create("ContentPage", "Content Page")
                .AddProperty("title", "Title", DataTypes.TextString, "Content", true)
                .AddProperty("mainContent", "Main Content", DataTypes.RichtextEditor, "Content", true)
                .AddProperty("prTags", "PR Tags", "Press Release Tags", "Content", true)
                .SetParent(homepage)
                .Save();

            homepage.AddAllowedChildNodeType(dt).Save();
        }
    }
}