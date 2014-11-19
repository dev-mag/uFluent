namespace uFluent.Persistence
{
    public interface IUmbracoDatabaseAdaptor
    {
        object Insert(object poco);

        int Delete<T>(string sql, params object[] args);

        void Save(object poco);

        void Delete(object poco);

        void Update(object poco);

        T SingleOrDefault<T>(string sql, params object[] args);
    }
}
