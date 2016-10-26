namespace GameFrame.ServiceLocator
{
    public interface IServiceLocator
    {
        T GetService<T>();
    }
}
