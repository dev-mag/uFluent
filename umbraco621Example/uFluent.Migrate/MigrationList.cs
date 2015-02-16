using System.Collections.Generic;
using uFluent.Migrate;
using umbraco621Example.uFluent.Migrate.Migrations;

namespace umbraco621Example.uFluent.Migrate
{
    internal class MigrationList : IMigrationList
    {
        public IEnumerable<IUmbracoMigration> Migrations
        {
            get
            {
                return new List<IUmbracoMigration>
                {
                    new HomepageDocType(),
                    new PressReleaseTagsDataType(),
                    new HomepageAddPressReleaseTagsTest(),
                    new ContentDocTypeWithPRTagProperty(),
                    new UsefulLinksDataType(),
                    new ChangeUsefulLinksXPathFilterToContainContentPage(),
                    new ChangeUsefulLinksXPathFilterToContainNestedPage(),
                    new RemoveNestedPageFromUsefulLinksPicker()
                };
            }
        }
    }
}
