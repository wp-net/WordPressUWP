using System;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using WordPressUWP.Services;
using WordPressUWP.Interfaces;
using Windows.Storage;
using WordPressUWP.Helpers;
using Windows.UI.Xaml.Controls;
using System.Diagnostics;
using WordPressUWP.Models;

namespace WordPressUWP.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        // TODO WTS: Add other settings as necessary. For help see https://github.com/Microsoft/WindowsTemplateStudio/blob/master/docs/pages/settings.md
        private ElementTheme _elementTheme = ThemeSelectorService.Theme;
        private IPushNotificationService _pushNotificationService;
        private SettingsService _settingsService;

        public ElementTheme ElementTheme
        {
            get { return _elementTheme; }

            set { Set(ref _elementTheme, value); }
        }

        private string _versionDescription;

        public string VersionDescription
        {
            get { return _versionDescription; }

            set { Set(ref _versionDescription, value); }
        }

        private ICommand _switchThemeCommand;

        public ICommand SwitchThemeCommand
        {
            get
            {
                if (_switchThemeCommand == null)
                {
                    _switchThemeCommand = new RelayCommand<ElementTheme>(
                        async (param) =>
                        {
                            await ThemeSelectorService.SetThemeAsync(param);
                        });
                }

                return _switchThemeCommand;
            }
        }

        private int _fontSize;
        public int SelectedFontSize
        {
            get { return _fontSize; }
            set { Set(ref _fontSize, value); }
        }


        private bool _pushNotificationsEnabled;
        public bool PushNotificationsEnabled
        {
            get { return _pushNotificationsEnabled; }
            set { Set(ref _pushNotificationsEnabled, value); }
        }

        public SettingsViewModel(IPushNotificationService pushNotificationService, SettingsService settingsService)
        {
            _pushNotificationService = pushNotificationService;
            _settingsService = settingsService;
        }

        public void Initialize()
        {
            SelectedFontSize = _settingsService.GetSetting("fontsize", () => Config.DefaultFontSize, SettingLocality.Roamed);
            VersionDescription = GetVersionDescription();
            PushNotificationsEnabled = _settingsService.GetSetting(nameof(PushNotificationsEnabled), () => true, SettingLocality.Local);            
        }

        private string GetVersionDescription()
        {
            var package = Package.Current;
            var packageId = package.Id;
            var version = packageId.Version;

            return $"{package.DisplayName} - {version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
        }

        public async void ChangePushNotificationSettings(object sender, RoutedEventArgs e)
        {
            if (sender is ToggleSwitch ts && Config.NotificationsEnabled)
            {
                _settingsService.SetSetting(nameof(PushNotificationsEnabled), ts.IsOn, SettingLocality.Local);
                if (ts.IsOn)
                    PushNotificationsEnabled = await _pushNotificationService.EnablePushNotifications();
                else
                    await _pushNotificationService.DisablePushNotificaitons();
            }
        }

        public void ChangeFontSizeSettings(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine($"saving font size {SelectedFontSize}");
            _settingsService.SetSetting("fontsize", SelectedFontSize, SettingLocality.Roamed);
        }
    }
}
