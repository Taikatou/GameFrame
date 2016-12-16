using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using GameFrame.Controllers;
using GameFrame.PathFinding.PossibleMovements;
using GameFrame.ServiceLocator;
using GameFrame.Services;

namespace Demos.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage : Page
    {
		private readonly DemoGame _game;

		public GamePage()
        {
            InitializeComponent();

            StaticServiceLocator.AddService<ISaveAndLoad>(new SaveAndLoad());
            StaticServiceLocator.AddService<IControllerSettings>(new ControllerSettings());
            StaticServiceLocator.AddService<IPossibleMovements>(new FourWayPossibleMovement());
            // Create the game.
            var launchArguments = string.Empty;
            _game = MonoGame.Framework.XamlGame<DemoGame>.Create(launchArguments, Window.Current.CoreWindow, swapChainPanel);
        }
    }
}
