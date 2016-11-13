using GameFrame.Interceptor;

namespace GameFrame.Ink
{
    public abstract class StoryInterceptor : IInterceptor<StoryContext>
    {
        public abstract void Execute(StoryContext context);
    }
}
