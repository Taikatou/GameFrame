using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GameFrame.CollisionTest
{
    public interface IObserver
    {
        void Update(string type);
    }
}
