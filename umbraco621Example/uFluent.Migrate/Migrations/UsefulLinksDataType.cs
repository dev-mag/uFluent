using uFluent;
using uFluent.Consts;
using uFluent.Extensions.MultiNodeTreePicker;
using uFluent.Extensions.MultiNodeTreePicker.Enums;
using uFluent.Migrate;
using Umbraco.Core.Models;

namespace umbraco621Example.uFluent.Migrate.Migrations
{
    internal class UsefulLinksDataType : IUmbracoMigration
    {
        public void Migrate()
        {
            var dt = DataType.Create(CustomDataTypeConsts.UsefulLinksPicker, PropertyEditor.MultiNodeTreePicker, DataTypeDatabaseType.Ntext)
                .SetMultiNodeTreePickerPreValues
                (
                    startNodeXPathExpression: "$currentPage/ancestor-or-self::Site[@isDoc]",
                    startNodeSelectionType: StartNodeSelectionType.XPathExpression,
                    xPathFilter: "Homepage",
                    useXPath: true
                )
                .Save();      
        }
    }
}