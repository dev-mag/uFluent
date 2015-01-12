using uFluent;
using uFluent.Migrate;

namespace uFluentExample.uFluent.Migrate.Migrations
{
    public class Article : IUmbracoMigration
    {
        public void Migrate()
        {
            var homepageDocType = DocumentType.Get("Homepage");

            var categoryDocType = DocumentType.Get("category");

            var articleTemplate = Template.Create("article", "Article");
            articleTemplate.Save();

            var articleDocType = DocumentType.Create("article", "Article")
                .SetParent(homepageDocType)
                .SetDefaultTemplate(articleTemplate)
                .Save();

            categoryDocType.AddAllowedChildNodeType(articleDocType)
            .Save();
        }
    }
}