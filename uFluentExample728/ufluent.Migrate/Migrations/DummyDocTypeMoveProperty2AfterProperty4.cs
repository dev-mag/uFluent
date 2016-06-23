using uFluent;
using uFluent.Migrate;

namespace uFluentExample.uFluent.Migrate.Migrations
{
    public class DummyDocTypeMoveProperty2AfterProperty4 : IUmbracoMigration
    {
        public void Migrate()
        {
            var dt = DocumentType.Get("dummyDocumentType")
                .SetPropertyAfter("Property2", "Property4")
                .Save();
        }
    }
}