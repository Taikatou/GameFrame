using System;
using System.Collections.Generic;
using System.Linq;
using GameFrame.Controllers.GamePad;
using GameFrame.Controllers.KeyBoard;
using GameFrame.Controllers.SmartButton;
using GameFrame.Movers;
using GameFrame.PathFinding.PossibleMovements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;

namespace GameFrame.Controllers
{
    public class EntityController : IUpdate
    {
        public enum Directions { Left, Right, Up, Down}
        private readonly SmartController _smartController;
        public int ButtonsDown;
        public bool PlayerMove => ButtonsDown != 0;
        public BaseMovable ToMove;
        private readonly IPossibleMovements _possibleMovements;

        public Vector2 MovingDirection
        {
            get { return ToMove.MovingDirection; }
            set { ToMove.MovingDirection = value; }
        }

        public EntityController(BaseMovable baseMovable, IPossibleMovements possibleMovements, MoverManager moverManager, Dictionary<Directions, List<IButtonAble>> directionButtons)
        {
            _possibleMovements = possibleMovements;
            ToMove = baseMovable;
            _smartController = new SmartController();
            CreateCompositeButton(directionButtons[Directions.Up], baseMovable, new Vector2(0, -1), moverManager);
            CreateCompositeButton(directionButtons[Directions.Down], baseMovable, new Vector2(0, 1), moverManager);
            CreateCompositeButton(directionButtons[Directions.Left], baseMovable, new Vector2(-1, 0), moverManager);
            CreateCompositeButton(directionButtons[Directions.Right], baseMovable, new Vector2(1, 0), moverManager);
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
            var allowedMovements = _possibleMovements.GetAdjacentLocations(moveFrom.ToPoint());
            var endPoint = (moveFrom + requeustedMovement).ToPoint();
            var possible = allowedMovements.Contains(endPoint);
            if (possible)
            {
                MovingDirection = requeustedMovement;
            }
        }

        private CompositeSmartButton CreateCompositeButton(List<IButtonAble> buttons, BaseMovable entityMover, Vector2 moveBy, MoverManager moverManager)
        {
            var smartButton = new CompositeSmartButton(buttons);
            smartButton.OnButtonJustPressed += (sender, args) =>
            {
                ButtonsDown++;
                ToMove.Moving = PlayerMove;
                moverManager.RemoveMover(entityMover);
                MoveBy(moveBy, entityMover.Position);
            };
            smartButton.OnButtonHeldDown += (sender, args) =>
            {
                ToMove.Moving = PlayerMove;
                MoveBy(moveBy, entityMover.Position);
            };
            smartButton.OnButtonReleased += (sender, args) =>
            {
                ButtonsDown--;
                ToMove.Moving = PlayerMove;
                moverManager.RemoveMover(entityMover);
                Release(moveBy);
            };
            _smartController.AddButton(smartButton);

            return smartButton;
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
