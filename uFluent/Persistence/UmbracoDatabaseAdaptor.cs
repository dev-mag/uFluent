using Umbraco.Core;
using Umbraco.Core.Persistence;

namespace uFluent.Persistence
{
    internal class UmbracoDatabaseAdaptor : IUmbracoDatabaseAdaptor
    {
        private UmbracoDatabase UmbracoDatabase { get; set; }

        public UmbracoDatabaseAdaptor()
        {
            if (ApplicationContext.Current == null)
            {
                throw new FluentException("Current Umbraco application context is not loaded.");
            }

            var databaseContext = ApplicationContext.Current.DatabaseContext;
            UmbracoDatabase = databaseContext.Database;
        }

        public object Insert(object poco)
        {
            return UmbracoDatabase.Insert(poco);
        }

        public int Delete<T>(string sql, params object[] args)
        {
            return UmbracoDatabase.Delete<T>(sql, args);
        }

        public void Save(object poco)
        {
            UmbracoDatabase.Save(poco);
        }

        public void Delete(object poco)
        {
            UmbracoDatabase.Delete(poco);
        }

        public void Update(object poco)
        {
            UmbracoDatabase.Update(poco);
        }

        public T SingleOrDefault<T>(string sql, params object[] args)
        {
            return UmbracoDatabase.SingleOrDefault<T>(sql, args);
        }
    }
}
