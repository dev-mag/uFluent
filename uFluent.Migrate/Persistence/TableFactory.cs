using log4net;
using Umbraco.Core;
using Umbraco.Core.Persistence;

namespace uFluent.Migrate.Persistence
{
    public static class TableFactory
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof (TableFactory));

        public static void CreateTables()
        {
            var databaseContext = ApplicationContext.Current.DatabaseContext;

            if (!databaseContext.IsDatabaseConfigured)
            {
                Log.Debug("Database is not configured, skipping uFluent.Migrate table creation");
                return;
            }

            var database = databaseContext.Database;

            try
            {
                database.OpenSharedConnection();
                
                if (database.TableExist("MigrationHistory"))
                {
                    Log.Debug("MigrationHistory table already exists.");
                    return;
                }

                using (var transaction = database.GetTransaction())
                {
                    database.CreateTable<MigrationHistory>();
                    transaction.Complete();
                    Log.Info("Created MigrationHistory table.");
                }
            }
            finally
            {
                database.CloseSharedConnection();
            }
        }
    }
}