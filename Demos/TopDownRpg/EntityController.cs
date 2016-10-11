using System.Collections.Generic;
using GameFrame.CollisionSystems.SpatialHash;
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
        private readonly Entity _entity;
        private readonly SmartController _smartController;
        private readonly ExpiringSpatialHashCollisionSystem<Entity> _spatialHashLayer;
        public int ButtonsDown;
        public bool PlayerMove => ButtonsDown != 0;

        public bool EntityMoving
        {
            get
            {
                var position = _entity.Position.ToPoint();
                var entityMoving = _spatialHashLayer.Moving(position);
                return entityMoving;;
            }
        }

        public EntityController(Entity entity, IMover entityMover, ExpiringSpatialHashCollisionSystem<Entity> spatialHashLayer)
        {
            _entity = entity;
            _spatialHashLayer = spatialHashLayer;
            _smartController = new SmartController();
            var upButtons = new List<IButtonAble> { new KeyButton(Keys.W), new KeyButton(Keys.Up), new DirectionGamePadButton(Buttons.DPadUp) };
            CreateCompositeButton(upButtons, entityMover, new Vector2(0, -1));

            var downButtons = new List<IButtonAble> { new KeyButton(Keys.S), new KeyButton(Keys.Down), new DirectionGamePadButton(Buttons.DPadDown) };
            CreateCompositeButton(downButtons, entityMover, new Vector2(0, 1));

            var leftButtons = new List<IButtonAble> { new KeyButton(Keys.A), new KeyButton(Keys.Left), new DirectionGamePadButton(Buttons.DPadLeft) };
            CreateCompositeButton(leftButtons, entityMover, new Vector2(-1, 0));

            var rightButtons = new List<IButtonAble> { new KeyButton(Keys.D), new KeyButton(Keys.Right), new DirectionGamePadButton(Buttons.DPadRight) };
            CreateCompositeButton(rightButtons, entityMover, new Vector2(1, 0));
        }

        public void CreateCompositeButton(List<IButtonAble> buttons, IMover entityMover, Vector2 direction)
        {
            var smartButton = new CompositeSmartButton();
            foreach (var button in buttons)
            {
                smartButton.AddButton(button);
            }
            smartButton.OnButtonJustPressed = (sender, args) =>
            {
                ButtonsDown++;
                if (!EntityMoving)
                {
                    _entity.Direction = direction;
                    _entity.Moving = PlayerMove;
                }
            };
            smartButton.OnButtonHeldDown = (sender, args) =>
            {
                if (!EntityMoving && ButtonsDown == 1)
                {
                    _entity.Direction = direction;
                    _entity.Moving = PlayerMove;
                }
            };
            smartButton.OnButtonReleased = (sender, args) =>
            {
                ButtonsDown--;
                _entity.Moving = PlayerMove;
            };
            _smartController.AddButton(smartButton);
        }

        public void Update(GameTime gameTime)
        {
            _smartController.Update(gameTime);
        }
    }
}
