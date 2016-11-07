using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Demos.Common;
using Demos.MobileShared;
using GameFrame.Controllers;
using GameFrame.ServiceLocator;
using GameFrame.Services;
using HockeyApp.Android;
using HockeyApp.Android.Metrics;

namespace Demos.Droid
{
    [Activity(Label = "Demos.Droid"
        , MainLauncher = true
        , Icon = "@drawable/icon"
        , Theme = "@style/Theme.Splash"
        , AlwaysRetainTaskState = true
        , LaunchMode = LaunchMode.SingleInstance
        , ScreenOrientation = ScreenOrientation.SensorLandscape
        , ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenSize)]
    public class Activity1 : Microsoft.Xna.Framework.AndroidGameActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            StaticServiceLocator.AddService<IControllerSettings>(new ControllerSettings());
            StaticServiceLocator.AddService<ISaveAndLoad>(new SaveAndLoad(Assets));
            CrashManager.Register(this, IdManager.HockeyAppId);
            MetricsManager.Register(Application, IdManager.HockeyAppId);
            var g = new DemoGame();
            SetContentView((View)g.Services.GetService(typeof(View)));
            g.Run();
        }
    }
}

