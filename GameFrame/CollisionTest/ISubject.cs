namespace GameFrame.CollisionTest
{
    public interface ISubject
    {
        void RegisterObserver(IObserver observer);
        void UnRegisterObserver(IObserver observer);
        void NotifyObservers();
    }
}
