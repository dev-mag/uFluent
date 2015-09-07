using uFluent;
using uFluent.Extensions.MultiNodeTreePicker;
using uFluent.Migrate;
using uFluentExample.uFluent.Migrate;

namespace uFluentExample.ufluent.Migrate.Migrations.UsefulLinksPicker
{
    public class UsefulLinksPickerAddHomepageCategoryArticleDocTypes : IUmbracoMigration
    {
        public void Migrate()
        {
            DataType.Get(CustomDataTypeConsts.UsefulLinksPicker).AddDocTypeToXPathFilter("Category", "Article").Save();
        }
    }
}