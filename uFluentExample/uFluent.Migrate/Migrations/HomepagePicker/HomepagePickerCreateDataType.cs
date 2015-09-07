using uFluent;
using uFluent.Consts;
using uFluent.Extensions.MultiNodeTreePicker;
using uFluent.Extensions.MultiNodeTreePicker.Enums;
using uFluent.Extensions.MultiNodeTreePicker.Models;
using uFluent.Migrate;
using Umbraco.Core.Models;

namespace uFluentExample.uFluent.Migrate.Migrations.HomepagePicker
{
    public class HomepagePickerCreateDataType : IUmbracoMigration
    {
        public void Migrate()
        {
            DataType.Create(CustomDataTypeConsts.HomepagePicker, PropertyEditor.MultiNodeTreePicker, DataTypeDatabaseType.Ntext)
                .SetMultiNodeTreePickerPreValues(new MultiNodeTreePickerPreValues(NodeType.Content, null, "$site", null, 1, null, false))
                .Save();
        }
    }
}