using System;
using MonoGame.Extended.ViewportAdapters;

namespace Demos.Screens
{
    public class VideoOptionsScreen : MenuScreen
    {
        public VideoOptionsScreen(ViewportAdapter viewPort, IServiceProvider serviceProvider)
            : base(viewPort, serviceProvider)
        {
        }

        public override void LoadContent()
        {
            base.LoadContent();

            AddMenuItem("Back", Show<OptionsScreen>);
        }
    }
}