using System;
using System.Collections.Generic;
using System.Linq;
using GameFrame.Controllers.SmartButton;
using GameFrame.Movers;
using GameFrame.PathFinding.PossibleMovements;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace GameFrame.Controllers
{
    public class BaseMovableController : IUpdate
    {
        public enum Directions { Left, Right, Up, Down}
        private readonly SmartController _smartController;
        public int ButtonsDown;
        public bool PlayerMove => ButtonsDown != 0;
        public BaseMovable ToMove;
        private readonly IPossibleMovements _possibleMovements;
        public Vector2 MovementCircle;
        public Vector2 MovingDirection
        {
            get { return ToMove.MovingDirection; }
            set { ToMove.MovingDirection = value; }
        }

        public BaseMovableController(BaseMovable baseMovable, IPossibleMovements possibleMovements, MoverManager moverManager, Dictionary<Directions, List<IButtonAble>> directionButtons, Vector2 movementCircle)
        {
            MovementCircle = movementCircle;
            _possibleMovements = possibleMovements;
            ToMove = baseMovable;
            _smartController = new SmartController();
            CreateCompositeButton(directionButtons[Directions.Up], baseMovable, new Vector2(0, -movementCircle.Y), moverManager);
            CreateCompositeButton(directionButtons[Directions.Down], baseMovable, new Vector2(0, movementCircle.Y), moverManager);
            CreateCompositeButton(directionButtons[Directions.Left], baseMovable, new Vector2(-movementCircle.X, 0), moverManager);
            CreateCompositeButton(directionButtons[Directions.Right], baseMovable, new Vector2(movementCircle.X, 0), moverManager);
        }

        private void Release(Vector2 releaseBy)
        {
            const float tolerance = 0.1f;
            if (Math.Abs(MovingDirection.X - releaseBy.X) < tolerance)
            {
                MovingDirection = new Vector2(0, MovingDirection.Y);
            }
            if (Math.Abs(MovingDirection.Y - releaseBy.Y) < tolerance)
            {
                MovingDirection = new Vector2(MovingDirection.X, 0);
            }
        }

        private void MoveBy(Vector2 moveBy, Vector2 moveFrom)
        {
            var requeustedMovement = MovingDirection + moveBy;
            var allowedMovements = _possibleMovements.GetAdjacentLocations(moveFrom.ToPoint(), MovementCircle.ToPoint());
            var endPoint = (moveFrom + requeustedMovement).ToPoint();
            var possible = allowedMovements.Contains(endPoint);
            if (possible)
            {
                MovingDirection = requeustedMovement;
            }
        }

        private void CreateCompositeButton(List<IButtonAble> buttons, BaseMovable entityMover, Vector2 moveBy, MoverManager moverManager)
        {
            _smartController.AddButton(new CompositeSmartButton(buttons)
            {
                OnButtonJustPressed = (sender, args) =>
                {
                    ButtonsDown++;
                    ToMove.Moving = PlayerMove;
                    moverManager.RemoveMover(entityMover);
                    MoveBy(moveBy, entityMover.Position);
                },
                OnButtonHeldDown = (sender, args) =>
                {
                    ToMove.Moving = PlayerMove;
                    MoveBy(moveBy, entityMover.Position);
                },
                OnButtonReleased = (sender, args) =>
                {
                    ButtonsDown--;
                    ToMove.Moving = PlayerMove;
                    moverManager.RemoveMover(entityMover);
                    Release(moveBy);
                }
            });
        }

        public void AddButton(AbstractSmartButton button)
        {
            _smartController.AddButton(button);
        }

        public void Update(GameTime gameTime)
        {
            _smartController.Update(gameTime);
        }
    }
}
