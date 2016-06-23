﻿using uFluent;
using uFluent.Migrate;

namespace uFluentExample728.ufluent.Migrate.Migrations
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