using System;
using MonoGame.Extended.ViewportAdapters;

namespace Demos.Screens
{
    class HelpScreen : MenuScreen
    {
        public HelpScreen(ViewportAdapter viewPort, IServiceProvider serviceProvider) : base(viewPort, serviceProvider)
        {
        }

        public override void LoadContent()
        {
            base.LoadContent();
            AddMenuItem("Back", Show<MenuScreen>);
            AddMenuItem("Left joy stick = Move", () => { });
            AddMenuItem("D-Pad = Choose option", () => { });
            AddMenuItem("A talk", () => { });
            AddMenuItem("B Run", () => { });
        }
    }
}
