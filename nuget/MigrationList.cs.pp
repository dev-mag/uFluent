using System.Collections.Generic;
using uFluent.Migrate;

namespace $rootnamespace$.uFluent.Migrate
{
    internal class MigrationList : IMigrationList
    {
        public IEnumerable<IUmbracoMigration> Migrations
        {
            get
            {
                return new List<IUmbracoMigration>
                {
                    // new ExampleMigration()
                };
            }
        }
    }
}
