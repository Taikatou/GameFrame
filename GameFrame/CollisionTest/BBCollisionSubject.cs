using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GameFrame.CollisionTest
{
    public class BBCollisionSubject : ISubject
    {

        private List<IObserver> observers;
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

        /*public void SetVelocity(Vector2 velocity)
        {
            this.vel = velocity;
            NotifyObservers();
        }*/

        public void SetCollisionType(string type)
        {
            this.collisionType = type;
            NotifyObservers();
        }

        /*public Vector2 GetVelocity()
        {
            return vel;
        }*/

        public string GetCollisionType()
        {
            return collisionType;
        }
    }
}
