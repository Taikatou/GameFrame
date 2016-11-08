using System;
using MonoGame.Extended.ViewportAdapters;

namespace Demos.Screens
{
    public class OptionsScreen : MenuScreen
    {
        public OptionsScreen(ViewportAdapter viewPort, IServiceProvider serviceProvider)
            : base(viewPort, serviceProvider)
        {
        }

        public override void LoadContent()
        {
            base.LoadContent();

            AddMenuItem("Audio Options", Show<AudioOptionsScreen>);
            AddMenuItem("Video Options", Show<VideoOptionsScreen>);
            AddMenuItem("Keyboard Options", Show<KeyboardOptionsScreen>);
            AddMenuItem("Mouse Options", Show<MouseOptionsScreen>);
            AddMenuItem("Back", Show<MainMenuScreen>);
        }
    }
}