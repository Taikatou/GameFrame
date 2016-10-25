using System;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using GameFrame.Paths;

namespace GameFrame.Movers
{
    public class PathMover : IUpdate
    {
        public readonly AbstractPath Path;
        public BaseMovable ToMove;
        public bool Complete => !Path.ToMove;
        public EventHandler OnCompleteEvent;

        public PathMover(BaseMovable toMove, AbstractPath path) 
        {
            ToMove = toMove;
            Path = path;
            ToMove.Moving = true;
            ToMove.MovingDirection = new Vector2();
        }

        public void Update(GameTime gameTime)
        {
            var position = ToMove.Position.ToPoint();
            Path.Update(position);
            if (Path.ToMove)
            {
                var direction = Path.NextPosition - ToMove.Position.ToPoint();
                ToMove.MovingDirection = direction.ToVector2();
            }
            else if(ToMove.Moving)
            {
                ToMove.Moving = false;
                OnCompleteEvent?.Invoke(this, null);
            }
        }
    }
}
