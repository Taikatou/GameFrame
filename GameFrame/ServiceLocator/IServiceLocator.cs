namespace GameFrame.ServiceLocator
{
    public interface IServiceLocator
    {
        T GetService<T>();
        void AddService<T>(T service);
        bool ContainsService<T>();
    }
}
