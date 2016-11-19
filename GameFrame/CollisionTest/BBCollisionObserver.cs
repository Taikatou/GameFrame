using System.Diagnostics;

namespace GameFrame.CollisionTest
{
    public class BbCollisionObserver : IObserver
    {
        private string _collisionType;

        public BbCollisionObserver(ISubject subject)
        {
            subject.RegisterObserver(this);
        }


        public void Update(string type)
        {
            _collisionType = type;
            PrintData();
        }

        public void PrintData()
        {
            Debug.WriteLine("Collision Type : " + _collisionType);
        }
    }
}
