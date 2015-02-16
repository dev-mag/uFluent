using uFluent;
using uFluent.Extensions.MultiNodeTreePicker;
using uFluent.Migrate;
using uFluent.Utils.XPathFilters;

namespace umbraco621Example.uFluent.Migrate.Migrations
{
    public class ChangeUsefulLinksXPathFilterToContainContentPage : IUmbracoMigration
    {
        public void Migrate()
        {
            var mntp = DataType.Get(CustomDataTypeConsts.UsefulLinksPicker);

            var preValues = mntp.GetMultiNodeTreePickerPreValues();

            var xpathFilter = new DocTypeListXPath(preValues.XPathFilter);
            xpathFilter.Add("CurrentPage");

            preValues.XPathFilter = xpathFilter.ToString();

            mntp.SetMultiNodeTreePickerPreValues(preValues).Save();
        }
    }
}