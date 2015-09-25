using uFluent;
using uFluent.Migrate;
using uFluentExample.uFluent.Migrate;
using Umbraco.Core;
using Umbraco.Core.Models;

namespace uFluentExample.ufluent.Migrate.Migrations.UsefulLinksPicker
{
    public class UsefulLinksPickerChangeDbTypeToNvarchar : IUmbracoMigration
    {
        public void Migrate()
        {
            DataType.Get(CustomDataTypeConsts.UsefulLinksPicker).SetDataTypeDatabaseType(DataTypeDatabaseType.Nvarchar).Save();
        }
    }
}