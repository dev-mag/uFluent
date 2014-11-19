namespace uFluent.Dto
{
    /// <summary>
    /// PreValue DTO for serializing properties for XPath Drop Down List.
    /// </summary>
    public class XPathDropDownListPreValueDto
    {
        /// <summary>
        /// GUID for the content type. Use "C66BA18E-EAF3-4CFF-8A22-41B16D66A972" for Document,
        /// "B796F64C-1F99-4FFB-B886-4BF4BC011A9C" for Media. See UmbracoObjectType for more GUIDs.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Selector XPath
        /// </summary>
        public string XPath { get; set; }

        /// <summary>
        /// Store ID instead of node name.
        /// </summary>
        public bool UseId { get; set; }

        /// <summary>
        /// UmbracoObjectType value from UmbracoObjectType enum. 3 = Document, 4 = Media.
        /// </summary>
        public int UmbracoObjectType { get; set; }

        public static XPathDropDownListPreValueDto CreateForXPath(string xpath)
        {
            return new XPathDropDownListPreValueDto
            {
                Type = "C66BA18E-EAF3-4CFF-8A22-41B16D66A972",
                XPath = xpath,
                UseId = true,
                UmbracoObjectType = 3
            };
        }
    }
}