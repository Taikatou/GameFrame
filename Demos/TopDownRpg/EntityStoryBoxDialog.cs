using Demos.TopDownRpg.Entities;
using GameFrame.Ink;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Demos.TopDownRpg
{
    public class EntityStoryBoxDialog : StoryDialogBox
    {
        private Entity _interactingWith;
        private Vector2 _cachedPosition;
        private Entity _player => PlayerEntity.Instance;
        public EntityStoryBoxDialog(SpriteFont font) : base(font)
        {
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (!Complete && _cachedPosition != _player.Position)
            {
                EndDialog();   
            }
        }

        public override void EndDialog()
        {
            base.EndDialog();
            _interactingWith?.CompleteInteract();
        }

        public void StartStory(GameFrameStory story, Entity interactWith)
        {
            StartStory(story);
            _interactingWith = interactWith;
        }

        public override void StartStory(GameFrameStory story)
        {
            base.StartStory(story);
            _cachedPosition = _player.Position;
        }
    }
}
