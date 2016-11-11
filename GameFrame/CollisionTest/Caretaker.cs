using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrame.CollisionTest
{
    public class Caretaker
    {
        private readonly List<Memento> _mementoList = new List<Memento>();

        public void AddMemento(Memento state)
        {
            _mementoList.Add(state);
        }

        public Memento GetMemento(int index)
        {
            return _mementoList[index];
        }
    }
}
