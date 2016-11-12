using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GameFrame.CollisionTest
{
    public class Memento
    {

        private readonly Vector2 _position;

        public Memento(Vector2 objCurrent)
        {
            _position = objCurrent;
        }

        public Vector2 GetSavedPosition()
        {
            return _position;
        }
    }
}
