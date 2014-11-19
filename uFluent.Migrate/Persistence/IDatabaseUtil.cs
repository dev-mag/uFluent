using Umbraco.Core.Persistence;

namespace uFluent.Migrate.Persistence
{
    public interface IDatabaseUtil
    {
        void FlagMigrationAsStarted(IUmbracoMigration migration);
        bool PreviousMigrationsHaveFinishedCleanly();
        bool HasMigrationExecuted(IUmbracoMigration migration);
        void FlagMigrationAsFinished(IUmbracoMigration migration);
        UmbracoDatabase UmbracoDatabase { get; }
        Transaction GetTransaction();
    }
}