using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace GameFrame.Movers
{
    public class MoverManager : IUpdate
    {
        public Dictionary<BaseMovable, PathMover> Movers;

        public MoverManager()
        {
            Movers = new Dictionary<BaseMovable, PathMover>();
        }

        public void AddMover(PathMover mover)
        {
            Movers[mover.ToMove] = mover;
        }

        public void RemoveMover(BaseMovable baseMover)
        {
            if (Movers.ContainsKey(baseMover))
            {
                Movers[baseMover].Cancel();
                Movers.Remove(baseMover);
            }
        }

        public void Update(GameTime gameTime)
        {
            var toRemoveList = new List<BaseMovable>();
            foreach (var mover in Movers)
            {
                mover.Value.Update(gameTime);
                if (mover.Value.Complete)
                {
                    toRemoveList.Add(mover.Key);
                }
            }
            foreach (var toRemove in toRemoveList)
            {
                RemoveMover(toRemove);
            }
        }

        public List<Point> PathPoints(BaseMovable mover)
        {
            if (Movers.ContainsKey(mover))
            {
                return Movers[mover].Path.PathPoints;
            }
            else
            {
                return null;
            }
        }
    }
}
