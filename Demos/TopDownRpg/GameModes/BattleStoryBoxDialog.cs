using System;
using GameFrame.Ink;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace Demos.TopDownRpg.GameModes
{
    public class BattleStoryBoxDialog : StoryDialogBox
    {
        public EventHandler CompleteEvent;
        public BattleStoryBoxDialog(Size screenSize, SpriteFont font) : base(screenSize, font)
        {
        }

        public override void EndDialog()
        {
            base.EndDialog();
            CompleteEvent?.Invoke(this, null);
        }
    }
}
