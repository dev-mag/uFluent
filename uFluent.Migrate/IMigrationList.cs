using System.Collections.Generic;

namespace uFluent.Migrate
{
    public interface IMigrationList
    {
        IEnumerable<IUmbracoMigration> Migrations { get; }
    }
}