
namespace uFluent.Tests.Unit.Utils.XPathFilters
{
    public class DocTypeListXPathTestsConsts
    {
        // Valid doc type xpath filters
        public const string OneDocTypeDocListXPath = "Homepage";
        public const string TwoDocTypeDocListXPath = "Homepage | ContentPage";
        public const string ThreeDocTypeDocListXPath = "Homepage | ContentPage | NestedPage";
        public const string OneDocTypeUnderScore = "Home_page";
        public const string TwoDocTypeUnderscore = "Home_page | Content_Page";
        public const string TwoDocTypeSecondUnderscore = "Homepage | Content_Page";

        // Invalid doc type xpath filters
        public const string OneDocTypeSpecialCharacters = "H$mepage";
        public const string TwoDocTypeSpecialCharacters = "H£$epage | Cont£ntPage";
        public const string TwoDocTypeNonPipeDelimited = "Homepage ContentPage";
        public const string ThreeDocTypeSpecialCharacter = "Homepage | ContentPage | Nest£dPage";
        public const string ThreeDocTypeLastNodeNonPipeDelimited = "Homepage | ContentPage $ Nest£dPage";

        // Exception messages
        public const string DocTypeBeingAddedAlreadyExists = "The document type alias being added already exists.";
        public const string DocTypeBeingAddedIsEmptyOrNull = "Cannot add an empty doc type to XPath Filter list.  The document type alias MUST be specified.";
        public const string DocTypeBeingRemovedDoesntExist = "The document type alias being removed is not listed.";
        public const string DocTypeBeingRemovedIsEmptyOrNull = "Cannot remove an empty doc type from XPath Filter list.  The document type alias MUST be specified.";
        public const string AddDocTypeWithSpecialCharacterExceptionMessage = "Document type alias trying to add is not in a valid format.";
        public const string RemoveDocTypeWithSpecialCharacterExceptionMessage = "Document type alias trying to remove is not in a valid format.";
    }
}
