using System.Runtime.CompilerServices;
using System.Text;
using uFluent.Extensions.Enumeration;
using uFluent.Extensions.MultiNodeTreePicker.Enums;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Serialization;

namespace uFluent.Extensions.MultiNodeTreePicker.Models
{
    public class MultiNodeTreePickerPreValues
    {
        public MultiNodeTreePickerPreValues()
        {
            StartNode = new StartNode();
        }

        public MultiNodeTreePickerPreValues(NodeType nodeType, int? startNodeId, string startNodeXPathFilter, string allowedDocTypes, int? minSelectedNodes, int? maxSelectedNodes, bool showEditButton)
        {
            StartNode = new StartNode(nodeType, startNodeId, startNodeXPathFilter);
            StartNodeXPathFilter = startNodeXPathFilter;
            AllowedDocTypes = allowedDocTypes;
            MinSelectedNodes = minSelectedNodes;
            MaxSelectedNodes = maxSelectedNodes;
            ShowEditButton = showEditButton;
        }

        public StartNode StartNode { get; set; }
        public string StartNodeXPathFilter { get; set; }
        public string AllowedDocTypes { get; set; }
        public int? MinSelectedNodes { get; set; }
        public int? MaxSelectedNodes { get; set; }
        public bool ShowEditButton { get; set; }
    }
}