using System;
using System.Linq;
using Newtonsoft.Json;
using uFluent.Extensions.MultiNodeTreePicker.Models;
using uFluent.Utils.XPathFilters;

namespace uFluent.Extensions.MultiNodeTreePicker
{
    public static class MultiNodeTreePickerExtensions
    {
        public static IDataType SetMultiNodeTreePickerPreValues(this IDataType dataType, MultiNodeTreePickerPreValues multiNodeTreePickerPreValues)
        {
            if (dataType.GetDataTypePreValues().Any())
            {
                dataType.DeleteAllPreValues();
            }

            dataType.AddPreValue(multiNodeTreePickerPreValues.StartNode.ToJsonString(), 1, "startNode")
                .AddPreValue(multiNodeTreePickerPreValues.AllowedDocTypes, 2, "filter")
                .AddPreValue(multiNodeTreePickerPreValues.MinSelectedNodes.ToString(), 3, "minNumber")
                .AddPreValue(multiNodeTreePickerPreValues.MaxSelectedNodes.ToString(), 4, "maxNumber")
                .AddPreValue(multiNodeTreePickerPreValues.ShowEditButton ? "1" : "0", 5, "showEditButton");

            return dataType;
        }

        public static MultiNodeTreePickerPreValues GetMultiNodeTreePickerPreValues(this IDataType dataType)
        {
            try
            {
                var preValues = dataType.GetDataTypePreValues().ToArray();

                var multiNodePreValues = new MultiNodeTreePickerPreValues();

                var startNodeJson = (StartNodeJson) JsonConvert.DeserializeObject(preValues[0], typeof (StartNodeJson));

                multiNodePreValues.StartNode = startNodeJson.ToStartNode();
                multiNodePreValues.AllowedDocTypes = preValues[1];
                multiNodePreValues.MinSelectedNodes = string.IsNullOrEmpty(preValues[2]) ? (int?) null : int.Parse(preValues[2]);
                multiNodePreValues.MaxSelectedNodes = string.IsNullOrEmpty(preValues[3]) ? (int?) null : int.Parse(preValues[3]);
                multiNodePreValues.ShowEditButton = preValues[4] == "1";

                return multiNodePreValues;
            }
            catch (Exception ex)
            {
                throw new FluentException("Unable to parse pre values for the multinode tree picker: " + ex.Message);
            }
        }

        public static IDataType AddDocTypeToXPathFilter(this IDataType dataType, params string[] docTypes)
        {
            var preValues = dataType.GetMultiNodeTreePickerPreValues();

            var docTypeListXPath = new DocTypeListXPath(preValues.AllowedDocTypes);
            docTypeListXPath.Add(docTypes);

            preValues.AllowedDocTypes = docTypeListXPath.ToString();

            dataType.SetMultiNodeTreePickerPreValues(preValues);

            return dataType;
        }

        public static IDataType RemoveDocTypeFromXPathFilter(this IDataType dataType, params string[] docTypes)
        {
            var preValues = dataType.GetMultiNodeTreePickerPreValues();

            var docTypeListXPath = new DocTypeListXPath(preValues.AllowedDocTypes);
            docTypeListXPath.Remove(docTypes);

            preValues.AllowedDocTypes = docTypeListXPath.ToString();

            dataType.SetMultiNodeTreePickerPreValues(preValues);

            return dataType;
        }
    }
}