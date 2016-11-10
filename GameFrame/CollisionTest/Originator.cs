using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrame.CollisionTest
{
    public class Originator
    {

        private BBObject obj;

        public void set(BBObject newObj)
        {
            obj = newObj;
        }

        public Memento storeInMemento()
        {
            return new Memento(obj);
        }

        public BBObject restoreFromMemmento(Memento memento)
        {
            obj = memento.getSavedBBObject();
            return obj;
        }


    }
}
