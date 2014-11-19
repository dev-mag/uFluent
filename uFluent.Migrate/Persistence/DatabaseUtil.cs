using System;
using log4net;
using Umbraco.Core;
using Umbraco.Core.Persistence;

namespace uFluent.Migrate.Persistence
{
    public class DatabaseUtil : IDatabaseUtil
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof (DatabaseUtil));

        public void FlagMigrationAsStarted(IUmbracoMigration migration)
        {
            try
            {
                UmbracoDatabase.OpenSharedConnection();

                using (var transaction = GetTransaction())
                {
                    var row = new MigrationHistory
                    {
                        Completed = false,
                        Name = migration.GetType().Name,
                        Timestamp = DateTime.UtcNow
                    };

                    UmbracoDatabase.Insert(row);
                    transaction.Complete();
                }
            }
            finally
            {
                UmbracoDatabase.CloseSharedConnection();
            }
        }

        public bool PreviousMigrationsHaveFinishedCleanly()
        {
            try
            {
                UmbracoDatabase.OpenSharedConnection();

                using (var transaction = GetTransaction())
                {
                    var result = UmbracoDatabase.FirstOrDefault<MigrationHistory>("WHERE Completed = 0");
                    var previousMigrationsHaveFinishedCleanly = result == null;

                    transaction.Complete();
                    return previousMigrationsHaveFinishedCleanly;
                }
            }
            finally
            {
                UmbracoDatabase.CloseSharedConnection();
            }
        }

        public bool HasMigrationExecuted(IUmbracoMigration migration)
        {
            var migrationName = migration.GetType().Name;

            try
            {
                UmbracoDatabase.OpenSharedConnection();

                using (var transaction = GetTransaction())
                {
                    var migrationHistory = UmbracoDatabase.FirstOrDefault<MigrationHistory>("WHERE Name = @Name", new {Name = migrationName });
                    var hasMigrationExecuted = migrationHistory != null;

                    Log.Debug(string.Format("HasMigrationExecuted for {0} is {1}", migrationName, hasMigrationExecuted));

                    transaction.Complete();
                    return hasMigrationExecuted;
                }
            }
            finally
            {
                UmbracoDatabase.CloseSharedConnection();
            }
        }

        public void FlagMigrationAsFinished(IUmbracoMigration migration)
        {
            var migrationName = migration.GetType().Name;

            var migrationHistory = UmbracoDatabase.Single<MigrationHistory>("WHERE Name = @Name", new { Name = migrationName });
            migrationHistory.Completed = true;
            UmbracoDatabase.Update(migrationHistory);
        }

        public UmbracoDatabase UmbracoDatabase
        {
            get
            {
                var databaseContext = ApplicationContext.Current.DatabaseContext;
                var database = databaseContext.Database;
                return database;
            }
        }

        public Transaction GetTransaction()
        {
            var database = UmbracoDatabase;
            return database.GetTransaction();
        }
    }
}