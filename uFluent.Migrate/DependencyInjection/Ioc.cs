namespace uFluent.Migrate.DependencyInjection
{
    public static class Ioc
    {
        private static IIocContainer _iocContainer;

        public static void Initialize(IIocContainer iocContainer)
        {
            _iocContainer = iocContainer;
        }

        public static T Get<T>()
        {
            return _iocContainer.Get<T>();
        }
    }
}