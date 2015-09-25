using uFluent;
using uFluent.Migrate;
using uFluentExample.uFluent.Migrate;
using Umbraco.Core.Models;

namespace uFluentExample.ufluent.Migrate.Migrations.UsefulLinksPicker
{
    public class UsefulLinksPickerChangeDbTypeToNtext : IUmbracoMigration
    {
        public void Migrate()
        {
            DataType.Get(CustomDataTypeConsts.UsefulLinksPicker).SetDataTypeDatabaseType(DataTypeDatabaseType.Ntext).Save();
        }
    }
}