using uFluent;
using uFluent.Migrate;

namespace uFluentExample.uFluent.Migrate.Migrations
{
    public class DummyDocTypeSetPropertyPositionMinus1 : IUmbracoMigration
    {
        public void Migrate()
        {
            var dt = DocumentType.Get("dummyDocumentType")
                .SetPropertySortOrder("property5", -1, true)
                .Save();
        }
    }
}