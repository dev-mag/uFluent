using uFluent;
using uFluent.Consts;
using uFluent.Migrate;
using Umbraco.Core;

namespace uFluentExample.uFluent.Migrate.Migrations
{
    public class ArticleCreateTemplateAndDocType : IUmbracoMigration
    {
        public void Migrate()
        {
            var homepageDocType = DocumentType.Get("Homepage");

            var categoryDocType = DocumentType.Get("Category");

            var articleTemplate = Template.Create("Article", "Article");
            articleTemplate.Save();

            var articleDocType = DocumentType.Create("Article", "Article")
                .SetParent(homepageDocType)
                .SetDefaultTemplate(articleTemplate)
                .AddProperty("title", "Title", DataTypes.TextString, "Content", true)
                .AddProperty("content", "Content", DataTypes.RichtextEditor, "Content", true)
                .Save();

            categoryDocType.AddAllowedChildNodeType(articleDocType)
                           .Save();
        }
    }
}