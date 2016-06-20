using uFluent;
using uFluent.Migrate;

namespace uFluentExample.uFluent.Migrate.Migrations
{
    public class ArticleMoveContentAfterTitle : IUmbracoMigration
    {
        public void Migrate()
        {
            var dt = DocumentType.Get("Article")
                .SetPropertyAfter("content", "title")
                .Save();
        }
    }
}