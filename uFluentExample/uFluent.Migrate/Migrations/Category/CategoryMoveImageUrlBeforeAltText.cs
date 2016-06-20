using uFluent;
using uFluent.Migrate;

namespace uFluentExample.uFluent.Migrate.Migrations
{
    public class CategoryMoveImageUrlBeforeAltText : IUmbracoMigration
    {
        public void Migrate()
        {
            var dt = DocumentType.Get("Category")
                .SetPropertyBefore("iconImageUrl", "iconImageAltText")
                .Save();
        }
    }
}