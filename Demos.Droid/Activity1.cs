using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Demos.MobileShared;
using GameFrame.Controllers;
using GameFrame.ServiceLocator;
using GameFrame.Services;

namespace Demos.Droid
{
    [Activity(Label = "Demos.Droid"
        , MainLauncher = true
        , Icon = "@drawable/icon"
        , Theme = "@style/Theme.Splash"
        , AlwaysRetainTaskState = true
        , LaunchMode = Android.Content.PM.LaunchMode.SingleInstance
        , ScreenOrientation = ScreenOrientation.SensorLandscape
        , ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenSize)]
    public class Activity1 : Microsoft.Xna.Framework.AndroidGameActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            StaticServiceLocator.AddService<ISaveAndLoad>(new SaveAndLoad(Assets));
            StaticServiceLocator.AddService<IControllerSettings>(new ControllerSettings());
            var g = new DemoGame();
            SetContentView((View)g.Services.GetService(typeof(View)));
            g.Run();
        }
    }
}

