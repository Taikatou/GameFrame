using System;
using System.Collections.Generic;
using System.Linq;
using Demos.TopDownRpg.SpeedState;
using GameFrame.Controllers;
using GameFrame.Controllers.GamePad;
using GameFrame.Controllers.KeyBoard;
using GameFrame.Controllers.SmartButton;
using GameFrame.Movers;
using GameFrame.PathFinding.PossibleMovements;
using GameFrame.ServiceLocator;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;

namespace Demos.TopDownRpg
{
    public class EntityController : IUpdate
    {
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

        public EntityController(Entity entity, IPossibleMovements possibleMovements, MoverManager moverManager)
        {
            var controllerSettings = StaticServiceLocator.Instance.GetService<IControllerSettings>();
            _possibleMovements = possibleMovements;
            ToMove = entity;
            _smartController = new SmartController();
            var upButtons = new List<IButtonAble>
            {
                new KeyButton(Keys.W),
                new KeyButton(Keys.Up),
                new DirectionGamePadButton(Buttons.DPadUp)
            };
            CreateCompositeButton(upButtons, entity, new Vector2(0, -1), moverManager);

            var downButtons = new List<IButtonAble>
            {
                new KeyButton(Keys.S),
                new KeyButton(Keys.Down),
                new DirectionGamePadButton(Buttons.DPadDown)
            };
            CreateCompositeButton(downButtons, entity, new Vector2(0, 1), moverManager);

            var leftButtons = new List<IButtonAble>
            {
                new KeyButton(Keys.A),
                new KeyButton(Keys.Left),
                new DirectionGamePadButton(Buttons.DPadLeft)
            };
            CreateCompositeButton(leftButtons, entity, new Vector2(-1, 0), moverManager);

            var rightButtons = new List<IButtonAble>
            {
                new KeyButton(Keys.D),
                new KeyButton(Keys.Right),
                new DirectionGamePadButton(Buttons.DPadRight)
            };
            CreateCompositeButton(rightButtons, entity, new Vector2(1, 0), moverManager);

            var runningButton = new List<IButtonAble> {new KeyButton(Keys.B), new GamePadButton(Buttons.B)};
            var smartButton = new CompositeSmartButton();
            foreach (var button in runningButton)
            {
                smartButton.AddButton(button);
            }
            smartButton.OnButtonJustPressed = (sender, args) =>
            {
                entity.SpeedContext.SetSpeed(new SpeedRunning());
            };
            smartButton.OnButtonReleased = (sender, args) =>
            {
                entity.SpeedContext.SetSpeed(new SpeedNormal());
            };
            _smartController.AddButton(smartButton);
        }

        public void Release(Vector2 releaseBy)
        {
            var tolerance = 0.1f;
            if (Math.Abs(MovingDirection.X - releaseBy.X) < tolerance)
            {
                MovingDirection = new Vector2(0, MovingDirection.Y);
            }
            if (Math.Abs(MovingDirection.Y - releaseBy.Y) < tolerance)
            {
                MovingDirection = new Vector2(MovingDirection.X, 0);
            }
        }

        public void MoveBy(Vector2 moveTo, Vector2 moveFrom)
        {
            var requeustedMovement = MovingDirection + moveTo;
            var allowedMovements = _possibleMovements.GetAdjacentLocations(moveFrom.ToPoint());
            var endPoint = (moveFrom + requeustedMovement).ToPoint();
            var contains = allowedMovements.Contains(endPoint);
            MovingDirection = contains ? requeustedMovement : moveTo;
        }

        public CompositeSmartButton CreateCompositeButton(List<IButtonAble> buttons, BaseMovable entityMover, Vector2 moveBy, MoverManager moverManager)
        {
            var smartButton = new CompositeSmartButton();
            foreach (var button in buttons)
            {
                smartButton.AddButton(button);
            }
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

        public void Update(GameTime gameTime)
        {
            _smartController.Update(gameTime);
        }
    }
}
