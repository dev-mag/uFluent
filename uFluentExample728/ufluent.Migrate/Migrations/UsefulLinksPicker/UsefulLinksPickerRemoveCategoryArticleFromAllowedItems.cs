using uFluent;
using uFluent.Extensions.MultiNodeTreePicker;
using uFluent.Migrate;

namespace uFluentExample728.ufluent.Migrate.Migrations.UsefulLinksPicker
{
    public class UsefulLinksPickerRemoveCategoryArticleFromAllowedItems : IUmbracoMigration
    {
        public void Migrate()
        {
            DataType.Get(CustomDataTypeConsts.UsefulLinksPicker).RemoveDocTypeFromXPathFilter("Category", "Article").Save();
        }
    }
}