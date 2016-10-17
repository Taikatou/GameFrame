using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace GameFrame.Movers
{
    public class MoverManager : IUpdate
    {
        private readonly Dictionary<BaseMovable, PathMover> _movers;

        public MoverManager()
        {
            _movers = new Dictionary<BaseMovable, PathMover>();
        }

        public void AddMover(PathMover mover)
        {
            _movers[mover.ToMove] = mover;
        }

        public void RemoveMover(BaseMovable baseMover)
        {
            if (_movers.ContainsKey(baseMover))
            {
                _movers.Remove(baseMover);
            }
        }

        public void Update(GameTime gameTime)
        {
            var toRemoveList = new List<BaseMovable>();
            foreach (var mover in _movers)
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
    }
}
