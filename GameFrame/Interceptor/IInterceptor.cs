namespace GameFrame.Interceptor
{
    public interface IInterceptor<in TIn> where TIn : IContext
    {
        void Execute(TIn context);
    }
}
