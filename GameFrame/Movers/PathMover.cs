using Microsoft.Xna.Framework;
using MonoGame.Extended;
using GameFrame.Paths;

namespace GameFrame.Movers
{
    public class PathMover : IUpdate
    {
        private readonly IPath _path;
        public IMoving ToMove;

        public PathMover(IMoving toMove, IPath path) 
        {
            ToMove = toMove;
            _path = path;
            ToMove.Moving = true;
        }

        public void Update(GameTime gameTime)
        {
            if (_path.ToMove)
            {
                var position = ToMove.Position.ToPoint();
                _path.Update(position);
                if(_path.ToMove)
                {
                    var direction = _path.NextPosition - ToMove.Position.ToPoint();
                    ToMove.Direction = direction.ToVector2();
                }
            }
            else
            {
                ToMove.Moving = false;
            }
        }
    }
}
