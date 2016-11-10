using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrame.CollisionTest
{
    public class Memento
    {

        private BBObject obj;

        public Memento(BBObject objCurrent)
        {
            obj = objCurrent;
        }

        public BBObject getSavedBBObject()
        {
            return obj;
        }
    }
}
