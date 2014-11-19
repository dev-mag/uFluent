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
            this.UmbracoDatabase = databaseContext.Database;
        }

        public object Insert(object poco)
        {
            return this.UmbracoDatabase.Insert(poco);
        }

        public int Delete<T>(string sql, params object[] args)
        {
            return this.UmbracoDatabase.Delete<T>(sql, args);
        }

        public void Save(object poco)
        {
            this.UmbracoDatabase.Save(poco);
        }

        public void Delete(object poco)
        {
            this.UmbracoDatabase.Delete(poco);
        }

        public void Update(object poco)
        {
            this.UmbracoDatabase.Update(poco);
        }

        public T SingleOrDefault<T>(string sql, params object[] args)
        {
            return this.UmbracoDatabase.SingleOrDefault<T>(sql, args);
        }
    }
}
