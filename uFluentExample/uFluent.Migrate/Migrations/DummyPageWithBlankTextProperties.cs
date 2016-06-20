using uFluent;
using uFluent.Consts;
using uFluent.Migrate;

namespace uFluentExample.uFluent.Migrate.Migrations
{
    public class DummyPageWithBlankTextProperties : IUmbracoMigration
    {
        public void Migrate()
        {
            var template = Template.Create("dummyDocumentType", "Dummy Document Type");
            template.Save();

            var docType = DocumentType.Create("dummy Document Type", "Dummy Document Type")
                .AddAllowedTemplate(template)
                .SetDefaultTemplate(template)
                .AddProperty("property1", "Property 1", DataTypes.TextString, "Content", false)
                .AddProperty("property2", "Property 2", DataTypes.TextString, "Content", false)
                .AddProperty("property3", "Property 3", DataTypes.TextString, "Content", false)
                .AddProperty("property4", "Property 4", DataTypes.TextString, "Content", false)
                .AddProperty("property5", "Property 5", DataTypes.TextString, "Content", false)
                .Save();

            var homepage = DocumentType.Get("Homepage")
                .AddAllowedChildNodeType(docType)
                .Save();
        }
    }
}