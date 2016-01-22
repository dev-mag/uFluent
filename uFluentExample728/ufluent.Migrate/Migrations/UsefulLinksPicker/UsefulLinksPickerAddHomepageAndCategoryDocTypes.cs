using uFluent;
using uFluent.Extensions.MultiNodeTreePicker;
using uFluent.Migrate;

namespace uFluentExample728.ufluent.Migrate.Migrations.UsefulLinksPicker
{
    public class UsefulLinksPickerAddHomepageCategoryArticleDocTypes : IUmbracoMigration
    {
        public void Migrate()
        {
            DataType.Get(CustomDataTypeConsts.UsefulLinksPicker).AddDocTypeToXPathFilter("Category", "Article").Save();
        }
    }
}