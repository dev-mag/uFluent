using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using uFluent.Migrate.Persistence;
using uFluent.Configuration;

namespace uFluent.Migrate
{
    public class MigrationProcessor : IMigrationProcessor
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(MigrationProcessor));

        private readonly IDatabaseUtil _databaseUtil;

        public MigrationProcessor(IDatabaseUtil databaseUtil)
        {
            _databaseUtil = databaseUtil;
        }

        public void Run()
        {
            Log.Debug("uFluent.Migrate started");

            TableFactory.CreateTables();

            if (!uFluentSettings.Enabled)
            {
                Log.Info("Not enabled, skipping.");
                return;
            }

            MigrationHistory previouMigration;

            if (!_databaseUtil.PreviousMigrationsHaveFinishedCleanly(out previouMigration))
            {
                Log.Error(string.Format("Previous migration '{0}' at '{1}' did not finish cleanly, manual intervention required!", previouMigration.Name, previouMigration.Id));
                return;
            }

            var migrations = GetMigrations();
            ProcessMigrations(migrations);
        }

        private void ProcessMigrations(IEnumerable<IUmbracoMigration> migrations)
        {
            Log.Debug("Starting migrations.");
            try
            {
                foreach (var umbracoMigration in migrations)
                {
                    ProcessMigration(umbracoMigration);
                }
            }
            catch (Exception exception)
            {
                Log.Error("Migration failed", exception);
            }
        }

        private void ProcessMigration(IUmbracoMigration umbracoMigration)
        {
            if (_databaseUtil.HasMigrationExecuted(umbracoMigration))
                return;

            _databaseUtil.FlagMigrationAsStarted(umbracoMigration);

            _databaseUtil.UmbracoDatabase.OpenSharedConnection();

            try
            {
                _databaseUtil.UmbracoDatabase.BeginTransaction();

                Log.Info(string.Format("========== Executing migration {0} ==========",
                    umbracoMigration.GetType().FullName));

                umbracoMigration.Migrate();

                Log.Info(string.Format("---------- Executed migration {0} ----------",
                    umbracoMigration.GetType().FullName));

                _databaseUtil.FlagMigrationAsFinished(umbracoMigration);

                Log.Info("Transaction completed");
                _databaseUtil.UmbracoDatabase.CompleteTransaction();
            }
            catch (Exception exception)
            {
                Log.Error("Transaction aborted.", exception);
                _databaseUtil.UmbracoDatabase.AbortTransaction();
                throw;
            }
            finally
            {
                Log.Info("Shared connection closed.");
                _databaseUtil.UmbracoDatabase.CloseSharedConnection();
            }
        }

        private static IEnumerable<IUmbracoMigration> GetMigrations()
        {
            IMigrationList migrationList = null;

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes();
                foreach (var type in types)
                {
                    if (IsAMigrationList(type))
                    {
                        if (migrationList != null)
                            throw new InvalidOperationException(string.Format("Multiple implementations of IMigrationLists found, created {0} also found {1}", migrationList.GetType(), type));

                        migrationList = CreateInstance<IMigrationList>(type);
                    }
                }
            }

            if (migrationList == null)
                throw new InvalidOperationException("No implementation of IMigrationList found");

            var umbracoMigrations = migrationList.Migrations.ToList();

            Log.Debug(string.Format("Found {0} migrations", umbracoMigrations.Count));
            return umbracoMigrations;
        }

        private static T CreateInstance<T>(Type type)
        {
            return (T)type.GetConstructors()[0].Invoke(null);
        }

        private static bool IsAMigrationList(Type type)
        {
            return type.GetInterfaces().Contains(typeof(IMigrationList));
        }

    }
}