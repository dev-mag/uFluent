using uFluent;
using uFluent.Extensions.MultiNodeTreePicker;
using uFluent.Migrate;

namespace umbraco621Example.uFluent.Migrate.Migrations
{
    public class ChangeUsefulLinksXPathFilterToContainNestedPage : IUmbracoMigration
    {
        public void Migrate()
        {
            DataType.Get(CustomDataTypeConsts.UsefulLinksPicker).AddDocTypeToXPathFilter("NestedPage").Save();
        }
    }
}