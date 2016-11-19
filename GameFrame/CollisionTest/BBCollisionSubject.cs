using System.Collections.Generic;

namespace GameFrame.CollisionTest
{
    public class BbCollisionSubject : ISubject
    {
        private readonly List<IObserver> _observers;
        private string _collisionType;

        public BbCollisionSubject()
        {
            _observers = new List<IObserver>();
        }

        public void RegisterObserver(IObserver observer)
        {
            _observers.Add(observer);

        }

        public void UnRegisterObserver(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (IObserver ob in _observers)
            {
                ob.Update(_collisionType);
            }
        }

        public void SetCollisionType(string type)
        {
            _collisionType = type;
            NotifyObservers();
        }


        public string GetCollisionType()
        {
            return _collisionType;
        }
    }
}
