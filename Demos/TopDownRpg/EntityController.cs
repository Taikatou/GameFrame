using System.Collections.Generic;
using Demos.TopDownRpg.SpeedState;
using GameFrame.Controllers;
using GameFrame.Controllers.GamePad;
using GameFrame.Controllers.KeyBoard;
using GameFrame.Controllers.SmartButton;
using GameFrame.Movers;
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

        public Vector2 MovingDirection
        {
            get { return ToMove.MovingDirection; }
            set { ToMove.MovingDirection = value; }
        }

        public EntityController(Entity entity, MoverManager moverManager)
        {
            var controllerSettings = StaticServiceLocator.Instance.GetService<IControllerSettings>();
            ToMove = entity;
            _smartController = new SmartController();
            var upButtons = new List<IButtonAble>
            {
                new KeyButton(Keys.W),
                new KeyButton(Keys.Up),
                new DirectionGamePadButton(Buttons.DPadUp)
            };
            var upButton = CreateCompositeButton(upButtons, entity, moverManager);
            upButton.OnButtonJustPressed += (sender, args) =>
            {
                MovingDirection = new Vector2(MovingDirection.X, -1);
            };
            upButton.OnButtonReleased += (sender, args) =>
            {
                if (MovingDirection.Y == -1)
                {
                    MovingDirection = new Vector2(MovingDirection.X, 0);
                }
            };

            var downButtons = new List<IButtonAble>
            {
                new KeyButton(Keys.S),
                new KeyButton(Keys.Down),
                new DirectionGamePadButton(Buttons.DPadDown)
            };
            var downButton = CreateCompositeButton(downButtons, entity, moverManager);
            downButton.OnButtonJustPressed += (sender, args) =>
            {
                MovingDirection = new Vector2(MovingDirection.X, 1);
            };
            downButton.OnButtonReleased += (sender, args) =>
            {
                if (MovingDirection.Y == 1)
                {
                    MovingDirection = new Vector2(MovingDirection.X, 0);
                }
            };

            var leftButtons = new List<IButtonAble>
            {
                new KeyButton(Keys.A),
                new KeyButton(Keys.Left),
                new DirectionGamePadButton(Buttons.DPadLeft)
            };
            var leftButton = CreateCompositeButton(leftButtons, entity, moverManager);
            leftButton.OnButtonJustPressed += (sender, args) =>
            {
                MovingDirection = new Vector2(-1, MovingDirection.Y);
            };
            leftButton.OnButtonReleased += (sender, args) =>
            {
                if (MovingDirection.X == -1)
                {
                    MovingDirection = new Vector2(0, MovingDirection.Y);
                }
            };

            var rightButtons = new List<IButtonAble>
            {
                new KeyButton(Keys.D),
                new KeyButton(Keys.Right),
                new DirectionGamePadButton(Buttons.DPadRight)
            };
            var rightButton = CreateCompositeButton(rightButtons, entity, moverManager);
            rightButton.OnButtonJustPressed += (sender, args) =>
            {
                MovingDirection = new Vector2(1, MovingDirection.Y);
            };
            rightButton.OnButtonReleased += (sender, args) =>
            {
                if (MovingDirection.X == 1)
                {
                    MovingDirection = new Vector2(0, MovingDirection.Y);
                }
            };

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

        public CompositeSmartButton CreateCompositeButton(List<IButtonAble> buttons, BaseMovable entityMover, MoverManager moverManager)
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
