using uFluent;
using uFluent.Consts;
using uFluent.Migrate;

namespace uFluentExample728.ufluent.Migrate.Migrations.Category
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
                .AddProperty("iconImageUrl", "Icon Image Url", DataTypes.MediaPicker, "Content", true)
                .AddProperty("iconImageAltText", "Icon Image Alt Text", DataTypes.TextString, "Content", true)
                .Save();

            homepageDocType.AddAllowedChildNodeType(categoryDocType)
            .Save();

            categoryDocType.AddAllowedChildNodeType(categoryDocType)
                .Save();
        }
    }
}