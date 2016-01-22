using uFluent;
using uFluent.Migrate;
using Umbraco.Core.Models;

namespace uFluentExample728.ufluent.Migrate.Migrations.UsefulLinksPicker
{
    public class UsefulLinksPickerChangeDbTypeToNvarchar : IUmbracoMigration
    {
        public void Migrate()
        {
            DataType.Get(CustomDataTypeConsts.UsefulLinksPicker).SetDataTypeDatabaseType(DataTypeDatabaseType.Nvarchar).Save();
        }
    }
}