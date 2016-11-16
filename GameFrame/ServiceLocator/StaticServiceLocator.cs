namespace GameFrame.ServiceLocator
{
    public class StaticServiceLocator : IServiceLocator
    {
        private readonly IServiceLocator _serviceLocator;
        private static IServiceLocator _instance;
        public static IServiceLocator Instance => _instance ?? (_instance = new StaticServiceLocator());

        protected StaticServiceLocator()
        {
            _serviceLocator = new ServiceLocator();
        }

        public static T GetService<T>()
        {
            return Instance.GetService<T>();
        }

        void IServiceLocator.AddService<T>(T service)
        {
            _serviceLocator.AddService(service);
        }

        bool IServiceLocator.ContainsService<T>()
        {
            return _serviceLocator.ContainsService<T>();
        }

        public static bool ContainsService<T>()
        {
            return Instance.ContainsService<T>();
        }

        T IServiceLocator.GetService<T>()
        {
            return _serviceLocator.GetService<T>();
        }

        public static void AddService<T>(T service)
        {
            Instance.AddService<T>(service);
        }
    }
}
