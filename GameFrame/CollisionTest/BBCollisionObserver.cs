﻿using System.Diagnostics;

namespace GameFrame.CollisionTest
{
    public class BBCollisionObserver : IObserver
    {
        private string collisionType;
        private static int observerIDTracker = 0;
        private int _observerId;
        private ISubject _subject;

        public BBCollisionObserver(ISubject subject)
        {
            this._subject = subject;
            this._observerId = ++observerIDTracker;
            subject.RegisterObserver(this);
        }


        public void Update(string type)
        {
            this.collisionType = type;
            PrintData();
        }

        public void PrintData()
        {
            Debug.WriteLine("Collision Type : " + collisionType);
        }
    }
}
