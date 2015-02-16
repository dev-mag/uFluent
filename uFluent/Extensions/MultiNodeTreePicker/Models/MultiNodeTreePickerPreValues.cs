using uFluent.Extensions.MultiNodeTreePicker.Enums;

namespace uFluent.Extensions.MultiNodeTreePicker.Models
{
    public class MultiNodeTreePickerPreValues
    {
        public TreeType TreeType { get; set; }
        public string XPathFilter { get; set; }
        public int MaxNodeCount { get; set; }
        public bool ShowToolTip { get; set; }
        public bool StoreAsCommaDelimited { get; set; }
        public bool UseXPath { get; set; }
        public int StartNodeId { get; set; }
        public bool ShowThumbnails { get; set; }
        public StartNodeSelectionType StartNodeSelectionType { get; set; }
        public string StartNodeXPathExpression { get; set; }
        public StartNodeXPathExpressionType StartNodeXPathExpressionType { get; set; }
        public int ControlHeight { get; set; }
        public int MinNodeCount { get; set; }
        
        public MultiNodeTreePickerPreValues(TreeType treeType = TreeType.Content, string xPathFilter = "", int maxNodeCount = -1, bool showToolTip = true, bool storeAsCommaDelimited = true, bool useXPath = false, int startNodeId = -1, bool showThumbnails = false, StartNodeSelectionType startNodeSelectionType = StartNodeSelectionType.NodePicker, StartNodeXPathExpressionType startNodeXPathExpressionType = StartNodeXPathExpressionType.Global, string startNodeXPathExpression = "", int controlHeight = 200, int minNodeCount = 0)
        {
            TreeType = treeType;
            XPathFilter = xPathFilter;
            MaxNodeCount = maxNodeCount;
            ShowToolTip = showToolTip;
            StoreAsCommaDelimited = storeAsCommaDelimited;
            UseXPath = useXPath;
            StartNodeId = startNodeId;
            ShowThumbnails = showThumbnails;
            StartNodeSelectionType = startNodeSelectionType;
            StartNodeXPathExpressionType = startNodeXPathExpressionType;
            StartNodeXPathExpression = startNodeXPathExpression;
            ControlHeight = controlHeight;
            MinNodeCount = minNodeCount;
        }

    }
}