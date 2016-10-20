using System.Collections.Generic;
using GameFrame.Controllers;
using GameFrame.Controllers.GamePad;
using GameFrame.Controllers.KeyBoard;
using GameFrame.Controllers.SmartButton;
using GameFrame.Movers;
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

        public EntityController(Entity entity, BaseMovable entityMover, MoverManager moverManager)
        {
            ToMove = entity;
            _smartController = new SmartController();
            var upButtons = new List<IButtonAble> { new KeyButton(Keys.W), new KeyButton(Keys.Up), new DirectionGamePadButton(Buttons.DPadUp) };
            CreateCompositeButton(upButtons, entityMover, new Vector2(0, -1), moverManager);

            var downButtons = new List<IButtonAble> { new KeyButton(Keys.S), new KeyButton(Keys.Down), new DirectionGamePadButton(Buttons.DPadDown) };
            CreateCompositeButton(downButtons, entityMover, new Vector2(0, 1), moverManager);

            var leftButtons = new List<IButtonAble> { new KeyButton(Keys.A), new KeyButton(Keys.Left), new DirectionGamePadButton(Buttons.DPadLeft) };
            CreateCompositeButton(leftButtons, entityMover, new Vector2(-1, 0), moverManager);

            var rightButtons = new List<IButtonAble> { new KeyButton(Keys.D), new KeyButton(Keys.Right), new DirectionGamePadButton(Buttons.DPadRight) };
            CreateCompositeButton(rightButtons, entityMover, new Vector2(1, 0), moverManager);

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

        public void CreateCompositeButton(List<IButtonAble> buttons, BaseMovable entityMover, Vector2 direction, MoverManager moverManager)
        {
            var smartButton = new CompositeSmartButton();
            foreach (var button in buttons)
            {
                smartButton.AddButton(button);
            }
            smartButton.OnButtonJustPressed = (sender, args) =>
            {
                ButtonsDown++;
                ToMove.MovingDirection = direction;
                ToMove.Moving = PlayerMove;
                moverManager.RemoveMover(entityMover);
            };
            smartButton.OnButtonHeldDown = (sender, args) =>
            {
                ToMove.MovingDirection = direction;
                ToMove.Moving = PlayerMove;
            };
            smartButton.OnButtonReleased = (sender, args) =>
            {
                ButtonsDown--;
                ToMove.Moving = PlayerMove;
            };
            _smartController.AddButton(smartButton);
        }

        public void Update(GameTime gameTime)
        {
            _smartController.Update(gameTime);
        }
    }
}
