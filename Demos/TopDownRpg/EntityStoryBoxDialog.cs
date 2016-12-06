using Demos.TopDownRpg.Entities;
using GameFrame.Ink;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace Demos.TopDownRpg
{
    public class EntityStoryBoxDialog : StoryDialogBox
    {
        private AbstractEntity _interactingWith;
        private Vector2 _cachedPosition;
        public EntityStoryBoxDialog(Size screenSize, SpriteFont font) : base(screenSize, font)
        {
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            var player = PlayerEntity.Instance;
            if (!Complete && _cachedPosition != player.Position)
            {
                EndDialog();   
            }
        }

        public override void EndDialog()
        {
            base.EndDialog();
            _interactingWith?.CompleteInteract();
        }

        public void StartStory(GameFrameStory story, AbstractEntity interactWith)
        {
            StartStory(story);
            _interactingWith = interactWith;
        }

        public override void StartStory(GameFrameStory story)
        {
            base.StartStory(story);
            var player = PlayerEntity.Instance;
            _cachedPosition = player.Position;
        }
    }
}
