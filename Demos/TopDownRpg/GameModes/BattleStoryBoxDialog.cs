using System;
using GameFrame.Ink;
using MonoGame.Extended.BitmapFonts;

namespace Demos.TopDownRpg.GameModes
{
    public class BattleStoryBoxDialog : StoryDialogBox
    {
        public EventHandler CompleteEvent;
        public BattleStoryBoxDialog(BitmapFont font) : base(font)
        {
        }

        public override void EndDialog()
        {
            base.EndDialog();
            CompleteEvent?.Invoke(this, null);
        }
    }
}
