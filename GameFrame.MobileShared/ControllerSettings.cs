using GameFrame.Controllers;
using Refractored.Xam.Settings;
using Refractored.Xam.Settings.Abstractions;

namespace Demos.MobileShared
{
    public class ControllerSettings : IControllerSettings
    {
        private static ISettings AppSettings => CrossSettings.Current;

        private const string GamePadEnabledKey = "game_pad_enabled";
        private readonly bool GamePadEnabledDefault = false;

        public bool GamePadEnabled
        {
            get { return AppSettings.GetValueOrDefault(GamePadEnabledKey, GamePadEnabledDefault); }
            set { AppSettings.AddOrUpdateValue(GamePadEnabledKey, value); }
        }

        private const string KeyBoardMouseEnabledKey = "key_board_mouse_enabled";
        private readonly bool KeyBoardMouseEnabledDefault = false;

        public bool KeyBoardMouseEnabled
        {
            get { return AppSettings.GetValueOrDefault(KeyBoardMouseEnabledKey, KeyBoardMouseEnabledDefault); }
            set { AppSettings.AddOrUpdateValue(KeyBoardMouseEnabledKey, value); }
        }

        private const string TouchScreenEnabledKey = "touch_screen_enabled";
        private readonly bool TouchScreenEnabledDefault = true;

        public bool TouchScreenEnabled
        {
            get { return AppSettings.GetValueOrDefault(TouchScreenEnabledKey, TouchScreenEnabledDefault); }
            set { AppSettings.AddOrUpdateValue(TouchScreenEnabledKey, value); }
        }
    }
}