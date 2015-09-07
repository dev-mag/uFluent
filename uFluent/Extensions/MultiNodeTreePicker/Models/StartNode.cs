using Newtonsoft.Json;
using uFluent.Extensions.Enumeration;
using uFluent.Extensions.MultiNodeTreePicker.Enums;

namespace uFluent.Extensions.MultiNodeTreePicker.Models
{
    /// <summary>
    /// This is the pretty version that we want to use in our migrations
    /// </summary>
    public class StartNode
    {
        public StartNode(NodeType startNodeType, int? startNodeId, string startNodeXPathFilter)
        {
            StartNodeType = startNodeType;
            StartNodeId = startNodeId;
            XPathFilter = startNodeXPathFilter;
        }

        public StartNode()
        {
        }

        public NodeType StartNodeType { get; set; }
        public int? StartNodeId { get; set; }
        public string XPathFilter { get; set; }

        public string ToJsonString()
        {
            var jsonObject = new StartNodeJson
            {
                type = StartNodeType.GetDescription(),
                id = StartNodeId,
                query = XPathFilter
            };

            var settings = new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore};
            var jsonString = JsonConvert.SerializeObject(jsonObject, settings);
            return jsonString;
        }
    }

    /// <summary>
    /// This is used to serialize/deserialize Json for Umbraco
    /// </summary>
    public class StartNodeJson
    {
        public string type { get; set; }
        public int? id { get; set; }
        public string query { get; set; }

        public StartNode ToStartNode()
        {
            return new StartNode()
            {
                StartNodeType = (NodeType)EnumExtensions.GetValueFromDescription<NodeType>(type),
                StartNodeId = id,
                XPathFilter = query
            };
        }
    }
}