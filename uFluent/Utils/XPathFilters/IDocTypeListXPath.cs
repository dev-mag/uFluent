namespace uFluent.Utils.XPathFilters
{
    public interface IDocTypeListXPath
    {
        string ToString();
        void Add(params string[] docType);
        void Remove(params string[] docType);
        bool IsValid(string value);
    }
}