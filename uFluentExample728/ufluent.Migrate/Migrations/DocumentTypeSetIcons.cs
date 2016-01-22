using uFluent;
using uFluent.Migrate;

namespace uFluentExample728.uFluent.Migrate.Migrations
{
    public class DocumentTypeSetIcons : IUmbracoMigration
    {
        public void Migrate()
        {
            DocumentType.Get("Homepage").SetIcon("icon-home color-red").Save();

            DocumentType.Get("Category").SetIcon("airport.gif").Save();

            DocumentType.Get("Article").SetIcon("icon-article color-blue").Save();
        }
    }
}