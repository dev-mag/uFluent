using System;
using uFluent;
using uFluent.Migrate;
using umbraco.cms.businesslogic;

namespace uFluentExample.uFluent.Migrate.Migrations
{
    public class DummyDocTypeMoveProperty1BeforeProperty4 : IUmbracoMigration
    {
        public void Migrate()
        {
            var dt = DocumentType.Get("dummyDocumentType")
                .SetPropertyBefore("property1", "property4")
                .Save();
        }
    }
}