﻿using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using GameFrame.Paths;

namespace GameFrame.Movers
{
    public class PathMover : IUpdate, ICompleteAble
    {
        public readonly AbstractPath Path;
        public BaseMovable ToMove;
        public Point NextPosition;
        public bool Complete => !Path.ToMove;
        public EventHandler OnCompleteEvent { get; set; }
        public EventHandler OnCancelEvent { get; set; }
        public ICompleteAble MovementComplete;

        public void Cancel()
        {
            OnCancelEvent?.Invoke(this, null);
        }

        public PathMover(BaseMovable toMove, AbstractPath path, ICompleteAble movementComplete)
        {
            MovementComplete = movementComplete;
            ToMove = toMove;
            Path = path;
            ToMove.Moving = true;
            ToMove.MovingDirection = new Vector2();
            NextPosition = new Point();
        }

        public void Update(GameTime gameTime)
        {
            var position = ToMove.Position.ToPoint();
            Path.Update(position);
            if (Path.ToMove && ToMove.Position.ToPoint() != Path.NextPosition)
            {
                var direction = Path.NextPosition - ToMove.Position.ToPoint();
                NextPosition = Path.NextPosition;
                ToMove.Moving = true;
                ToMove.MovingDirection = direction.ToVector2();
            }
            else if (MovementComplete.Complete)
            {
                ToMove.OnMoveCompleteEvent += (sender, args) =>
                {
                    ToMove.Moving = false;
                    OnCompleteEvent?.Invoke(this, null);
                };
            }
        }
    }
}
