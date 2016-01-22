using uFluent;
using uFluent.Consts;
using uFluent.Extensions.MultiNodeTreePicker;
using uFluent.Extensions.MultiNodeTreePicker.Enums;
using uFluent.Extensions.MultiNodeTreePicker.Models;
using uFluent.Migrate;

namespace uFluentExample728.ufluent.Migrate.Migrations.UsefulLinksPicker
{
    public class UsefulLinksPickerCreateDataType : IUmbracoMigration
    {
        public void Migrate()
        {
            DataType.Create(CustomDataTypeConsts.UsefulLinksPicker, PropertyEditor.MultiNodeTreePicker)
                .SetMultiNodeTreePickerPreValues(new MultiNodeTreePickerPreValues(NodeType.Content, null, null, "Homepage", 1, null, false))
                .Save();
        }
    }
}