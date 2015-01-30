using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using uFluent;
using uFluent.Migrate;

namespace uFluentExample.uFluent.Migrate.Migrations
{
    public class Category : IUmbracoMigration
    {
        public void Migrate()
        {
            var homepageDocType = DocumentType.Get("Homepage");

            var categoryTemplate = Template.Create("category", "Category");
            categoryTemplate.Save();

            var categoryDocType = DocumentType.Create("category", "Category")
                .SetParent(homepageDocType)
                .SetDefaultTemplate(categoryTemplate)
                .Save();
            
                homepageDocType.AddAllowedChildNodeType(categoryDocType)
                .Save();

            categoryDocType.AddAllowedChildNodeType(categoryDocType)
                .Save();
        }
    }
}