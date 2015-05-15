using uFluent;
using uFluent.Consts;
using uFluent.Migrate;
using Umbraco.Core;

namespace uFluentExample.uFluent.Migrate.Migrations
{
    public class CategoryAddIcon : IUmbracoMigration
    {
        public void Migrate()
        {
            DocumentType.Get("category")
                .AddProperty("iconImageUrl", "Icon Image Url", DataTypes.MediaPicker, "Content", true)
                .AddProperty("iconImageAltText", "Icon Image Alt Text", DataTypes.TextString, "Content", true)
                .Save();
        }
    }
}