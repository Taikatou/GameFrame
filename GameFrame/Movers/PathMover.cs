using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace GameFrame.Movers
{
    public class PathMover : IUpdate
    {
        private readonly List<Point> _pathList;
        public IMoving ToMove;

        public PathMover(IMoving toMove, List<Point> pathList) 
        {
            ToMove = toMove;
            _pathList = pathList;
            ToMove.Moving = true;
        }

        public void Update(GameTime gameTime)
        {
            if (_pathList.Count > 0)
            {
                if (ToMove.Position.ToPoint() == _pathList[0])
                {
                    _pathList.RemoveAt(0);
                }
            }
            if (_pathList.Count > 0)
            {
                var direction = _pathList[0] - ToMove.Position.ToPoint();
                Debug.WriteLine(direction);
                ToMove.Direction = direction.ToVector2();
            }
        }
    }
}
