namespace uFluent.Migrate.DependencyInjection
{
    public interface IIocContainer
    {
        T Get<T>();
    }
}