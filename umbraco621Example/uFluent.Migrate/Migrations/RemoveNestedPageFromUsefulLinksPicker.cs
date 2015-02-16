using uFluent;
using uFluent.Extensions.MultiNodeTreePicker;
using uFluent.Migrate;

namespace umbraco621Example.uFluent.Migrate.Migrations
{
    public class RemoveNestedPageFromUsefulLinksPicker : IUmbracoMigration
    {
        public void Migrate()
        {
            DataType.Get(CustomDataTypeConsts.UsefulLinksPicker).RemoveDocTypeFromXPathFilter("NestedPage").Save();
        }
    }
}