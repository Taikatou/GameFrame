﻿using GameFrame.Ink;
using GameFrame.Movers;
using Microsoft.Xna.Framework.Graphics;

namespace Demos.TopDownRpg
{
    public class EntityStoryBoxDialog : StoryDialogBox
    {
        private Entity _interactingWith;
        public EntityStoryBoxDialog(SpriteFont font, BaseMovable player) : base(font, player)
        {
        }

        public override void EndDialog()
        {
            base.EndDialog();
            _interactingWith?.CompleteInteract();
        }

        public void AddDialogBox(GameFrameStory story, Entity interactWith)
        {
            AddDialogBox(story);
            _interactingWith = interactWith;
        }
    }
}
