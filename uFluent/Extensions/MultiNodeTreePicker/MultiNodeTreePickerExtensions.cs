using System;
using System.Globalization;
using System.Linq;
using uFluent.Extensions.Enumeration;
using uFluent.Extensions.MultiNodeTreePicker.Enums;
using uFluent.Extensions.MultiNodeTreePicker.Models;
using uFluent.Utils.XPathFilters;
using umbraco.cms.presentation;

namespace uFluent.Extensions.MultiNodeTreePicker
{
    public static class MultiNodeTreePickerExtensions
    {
        [Obsolete("Use SetMultiNodeTreePickerPreValues instead")]
        public static DataType AddMultiNodeTreePickerPreValues(this DataType dataType,
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
            int minNodeCount = 0)
        {
            return SetMultiNodeTreePickerPreValues(dataType, 
                                            treeType,
                                            xPathFilter,
                                            maxNodeCount,
                                            showToolTip,
                                            storeAsCommaDelimited,
                                            useXPath,
                                            startNodeId,
                                            showThumbnails,
                                            startNodeSelectionType,
                                            startNodeXPathExpressionType,
                                            startNodeXPathExpression,
                                            controlHeight,
                                            minNodeCount);
        }


        public static DataType SetMultiNodeTreePickerPreValues(
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
            if (dataType.GetDataTypePreValues().Any())
            {
                dataType.DeleteAllPreValues();
            }

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

        public static DataType SetMultiNodeTreePickerPreValues(this DataType dataType, MultiNodeTreePickerPreValues multiNodeTreePickerPreValues)
        {
            return SetMultiNodeTreePickerPreValues
            (
                dataType,
                multiNodeTreePickerPreValues.TreeType,
                multiNodeTreePickerPreValues.XPathFilter,
                multiNodeTreePickerPreValues.MaxNodeCount, 
                multiNodeTreePickerPreValues.ShowToolTip, 
                multiNodeTreePickerPreValues.StoreAsCommaDelimited, 
                multiNodeTreePickerPreValues.UseXPath, 
                multiNodeTreePickerPreValues.StartNodeId, 
                multiNodeTreePickerPreValues.ShowThumbnails, 
                multiNodeTreePickerPreValues.StartNodeSelectionType, 
                multiNodeTreePickerPreValues.StartNodeXPathExpressionType, 
                multiNodeTreePickerPreValues.StartNodeXPathExpression, 
                multiNodeTreePickerPreValues.ControlHeight, 
                multiNodeTreePickerPreValues.MinNodeCount);
        }

        public static MultiNodeTreePickerPreValues GetMultiNodeTreePickerPreValues(this DataType dataType)
        {
            try
            {
                var preValues = dataType.GetDataTypePreValues().ToArray();

                return new MultiNodeTreePickerPreValues(
                    (TreeType)EnumExtensions.GetValueFromDescription<TreeType>(preValues[0]),
                    preValues[1],
                    int.Parse(preValues[2]),
                    bool.Parse(preValues[3]),
                    preValues[4].Equals("1"),
                    preValues[5].Equals("1"),
                    int.Parse(preValues[6]),
                    bool.Parse(preValues[7]),
                    (StartNodeSelectionType)Enum.Parse(typeof(StartNodeSelectionType), preValues[8]),
                    (StartNodeXPathExpressionType)Enum.Parse(typeof(StartNodeXPathExpressionType), preValues[9]),
                    preValues[10],
                    int.Parse(preValues[11]),
                    int.Parse(preValues[12])
                    );
            }
            catch(Exception ex)
            {
                throw new FluentException("Unable to parse pre values for the multinode tree picker: " + ex.Message);
            }
        }

        public static DataType AddDocTypeToXPathFilter(this DataType dataType, string docType)
        {
            var preValues = dataType.GetMultiNodeTreePickerPreValues();

            var docTypeListXPath = new DocTypeListXPath(preValues.XPathFilter);
            docTypeListXPath.Add(docType);

            preValues.XPathFilter = docTypeListXPath.ToString();

            dataType.SetMultiNodeTreePickerPreValues(preValues);

            return dataType;
        }

        public static DataType RemoveDocTypeFromXPathFilter(this DataType dataType, string docType)
        {
            var preValues = dataType.GetMultiNodeTreePickerPreValues();

            var docTypeListXPath = new DocTypeListXPath(preValues.XPathFilter);
            docTypeListXPath.Remove(docType);

            preValues.XPathFilter = docTypeListXPath.ToString();

            dataType.SetMultiNodeTreePickerPreValues(preValues);

            return dataType;
        }
    }
}