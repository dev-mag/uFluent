using System.Collections.Generic;
using uFluent.Migrate;

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
                    new Migrations.HomepageDocType(),
                    new Migrations.PressReleaseTagsDataType(),
                    new Migrations.HomepageAddPressReleaseTagsTest(),
                    new Migrations.ContentDocTypeWithPRTagProperty()
                };
            }
        }
    }
}
