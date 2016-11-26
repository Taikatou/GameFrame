using System;
using GameFrame.Ink;
using Microsoft.Xna.Framework.Graphics;

namespace Demos.TopDownRpg.GameModes
{
    public class BattleStoryBoxDialog : StoryDialogBox
    {
        public EventHandler CompleteEvent;
        public BattleStoryBoxDialog(SpriteFont font) : base(font)
        {
        }

        public override void EndDialog()
        {
            base.EndDialog();
            CompleteEvent?.Invoke(this, null);
        }
    }
}
