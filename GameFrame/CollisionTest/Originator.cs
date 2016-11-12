
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace GameFrame.CollisionTest
{
    public class Originator
    {

        private Vector2 _position;

        public void SetObject(Vector2 newObj)
        {
            Debug.WriteLine("Orig, Setting state");
            _position = newObj;
        }

        public Vector2 GetSavedPosition()
        {
            return _position;
        }

        public Memento CreateMemento()
        {
            Debug.WriteLine("Orig, Save to memento");
            return new Memento(_position);
        }

        public void GetStateFromMemento(Memento Memento)
        {
            Debug.WriteLine("Org, State after restoring from memento: "+ _position);
            _position = Memento.GetSavedPosition();
        }


    }
}
