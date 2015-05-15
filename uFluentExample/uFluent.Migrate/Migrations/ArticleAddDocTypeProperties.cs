using uFluent;
using uFluent.Consts;
using uFluent.Migrate;
using Umbraco.Core;
using Umbraco.Core.Models;

namespace uFluentExample.uFluent.Migrate.Migrations
{
    public class ArticleAddDocTypeProperties : IUmbracoMigration
    {
        public void Migrate()
        {
            DocumentType.Get("article")
                .AddProperty("title", "Title", DataTypes.TextString, "Content", true)
                .AddProperty("content", "Content", DataTypes.RichtextEditor, "Content", true)
                .Save();
        }
    }
}