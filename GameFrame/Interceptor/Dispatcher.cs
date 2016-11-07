using System.Collections.Generic;

namespace GameFrame.Interceptor
{
    public class Dispatcher<TIn> : IInterceptor<TIn> where TIn : IContext
    {
        public readonly List<IInterceptor<TIn>> Interceptors;

        public Dispatcher()
        {
            Interceptors = new List<IInterceptor<TIn>>();
        }

        public void AddInterceptor(IInterceptor<TIn> interceptor)
        {
            Interceptors.Add(interceptor);
        }

        public void RemoveInterceptor(IInterceptor<TIn> interceptor)
        {
            Interceptors.Remove(interceptor);
        }

        public void Execute(TIn context)
        {
            foreach (var interceptor in Interceptors)
            {
                interceptor.Execute(context);
            }
        }
    }
}
