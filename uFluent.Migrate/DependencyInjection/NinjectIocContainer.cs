using Ninject;

namespace uFluent.Migrate.DependencyInjection
{
    public class NinjectIocContainer : IIocContainer
    {
        private readonly StandardKernel _kernel;

        private NinjectIocContainer(StandardKernel kernel)
        {
            _kernel = kernel;
        }

        public T Get<T>()
        {
            return _kernel.Get<T>();
        }

        public static IIocContainer Create(StandardKernel kernel)
        {
            return new NinjectIocContainer(kernel);
        }
    }
}