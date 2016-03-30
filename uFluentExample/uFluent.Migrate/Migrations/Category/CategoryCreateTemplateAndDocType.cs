using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using uFluent;
using uFluent.Consts;
using uFluent.Migrate;

namespace uFluentExample.uFluent.Migrate.Migrations
{
    public class CategoryCreateTemplateAndDocType : IUmbracoMigration
    {
        public void Migrate()
        {
            var homepageDocType = DocumentType.Get("Homepage");

            var categoryTemplate = Template.Create("Category", "Category");
            categoryTemplate.Save();

            var categoryDocType = DocumentType.Create("Category", "Category")
                .SetParent(homepageDocType)
                .SetDefaultTemplate(categoryTemplate)
                .AddProperty("iconImageUrl", "Icon Image Url", DataTypes.MultipleMediaPicker, "Content", true)
                .AddProperty("iconImageAltText", "Icon Image Alt Text", DataTypes.TextString, "Content", true)
                .Save();

            homepageDocType.AddAllowedChildNodeType(categoryDocType)
            .Save();

            categoryDocType.AddAllowedChildNodeType(categoryDocType)
                .Save();
        }
    }
}