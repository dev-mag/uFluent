using uFluent;
using uFluent.Extensions.MultiNodeTreePicker;
using uFluent.Migrate;
using uFluentExample.uFluent.Migrate;

namespace uFluentExample.ufluent.Migrate.Migrations.UsefulLinksPicker
{
    public class UsefulLinksPickerRemoveCategoryArticleFromAllowedItems : IUmbracoMigration
    {
        public void Migrate()
        {
            DataType.Get(CustomDataTypeConsts.UsefulLinksPicker).RemoveDocTypeFromXPathFilter("Category", "Article").Save();
        }
    }
}