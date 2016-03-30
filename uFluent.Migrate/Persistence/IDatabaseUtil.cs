using Umbraco.Core.Persistence;

namespace uFluent.Migrate.Persistence
{
    public interface IDatabaseUtil
    {
        void FlagMigrationAsStarted(IUmbracoMigration migration);
        bool PreviousMigrationsHaveFinishedCleanly(out MigrationHistory previousMigration);
        bool HasMigrationExecuted(IUmbracoMigration migration);
        void FlagMigrationAsFinished(IUmbracoMigration migration);
        UmbracoDatabase UmbracoDatabase { get; }
        Transaction GetTransaction();
    }
}