using System.Globalization;
using uFluent.Extensions.MultiNodeTreePicker.Enums;

namespace uFluent.Extensions.MultiNodeTreePicker
{
    public static class MultiNodeTreePickerExtensions
    {
        public static DataType AddMultiNodeTreePickerPreValues(
            this DataType dataType,
            TreeType treeType = TreeType.Content,
            string xPathFilter = "",
            int maxNodeCount = -1,
            bool showToolTip = true,
            bool storeAsCommaDelimited = true,
            bool useXPath = false,
            int startNodeId = -1,
            bool showThumbnails = false,
            StartNodeSelectionType startNodeSelectionType = StartNodeSelectionType.NodePicker,
            StartNodeXPathExpressionType startNodeXPathExpressionType = StartNodeXPathExpressionType.Global,
            string startNodeXPathExpression = "",
            int controlHeight = 200,
            int minNodeCount = 0
            )
        {
            dataType
                .AddPreValue(treeType.ToString().ToLower())
                .AddPreValue(xPathFilter)
                .AddPreValue(maxNodeCount < 1 ? "-1" : maxNodeCount.ToString(CultureInfo.InvariantCulture))
                .AddPreValue(showToolTip ? "True" : "False")
                .AddPreValue(storeAsCommaDelimited ? "1" : "0")
                .AddPreValue(useXPath ? "1" : "0")
                .AddPreValue(startNodeId.ToString(CultureInfo.InvariantCulture))
                .AddPreValue(showThumbnails ? "True" : "False")
                .AddPreValue(((int) startNodeSelectionType).ToString(CultureInfo.InvariantCulture))
                .AddPreValue(((int) startNodeXPathExpressionType).ToString(CultureInfo.InvariantCulture))
                .AddPreValue(startNodeXPathExpression)
                .AddPreValue(controlHeight.ToString(CultureInfo.InvariantCulture))
                .AddPreValue(minNodeCount.ToString(CultureInfo.InvariantCulture));

            return dataType;
        }
    }
}