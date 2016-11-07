using System;
using MonoGame.Extended.ViewportAdapters;

namespace Demos.Screens
{
    public class LoadGameScreen : MenuScreen
    {
        public LoadGameScreen(ViewportAdapter viewPort, IServiceProvider serviceProvider)
            : base(viewPort, serviceProvider)
        {
        }

        public override void LoadContent()
        {
            base.LoadContent();

            AddMenuItem("Back", Show<MainMenuScreen>);
        }
    }
}