using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GameFrame.CollisionTest
{
    public class BBCollisionSubject : ISubject
    {

        private List<IObserver> observers;
        private Vector2 vel;
        private string collisionType;

        public BBCollisionSubject()
        {
            observers = new List<IObserver>();
        }

        public void RegisterObserver(IObserver observer)
        {
            observers.Add(observer);

        }

        public void UnRegisterObserver(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (IObserver ob in observers)
            {
                ob.Update(collisionType);
            }
        }

        public void SetCollisionType(string type)
        {
            this.collisionType = type;
            NotifyObservers();
        }


        public string GetCollisionType()
        {
            return collisionType;
        }
    }
}
