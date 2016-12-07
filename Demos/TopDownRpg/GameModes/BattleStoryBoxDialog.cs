using System;
using GameFrame.Ink;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace Demos.TopDownRpg.GameModes
{
    public class BattleStoryBoxDialog : StoryDialogBox
    {
        public EventHandler CompleteEvent;
        public BattleStoryBoxDialog(Size screenSize, SpriteFont font, bool gamePad) : base(screenSize, font, gamePad)
        {
        }

        public override void EndDialog()
        {
            base.EndDialog();
            CompleteEvent?.Invoke(this, null);
        }
    }
}
