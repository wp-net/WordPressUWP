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

namespace WordPressUWP.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        // TODO WTS: Add other settings as necessary. For help see https://github.com/Microsoft/WindowsTemplateStudio/blob/master/docs/pages/settings.md
        private ElementTheme _elementTheme = ThemeSelectorService.Theme;
        private IPushNotificationService _pushNotificationService;

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

        private bool _pushNotificationsEnabled;
        public bool PushNotificationsEnabled
        {
            get { return _pushNotificationsEnabled; }
            set { Set(ref _pushNotificationsEnabled, value); }
        }

        public SettingsViewModel(IPushNotificationService pushNotificationService)
        {
            _pushNotificationService = pushNotificationService;
        }

        public async void Initialize()
        {
            VersionDescription = GetVersionDescription();
            PushNotificationsEnabled = await ApplicationData.Current.LocalSettings.ReadAsync<bool>(nameof(PushNotificationsEnabled));
        }

        private string GetVersionDescription()
        {
            var package = Package.Current;
            var packageId = package.Id;
            var version = packageId.Version;

            return $"{package.DisplayName} - {version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
        }

        public async void ChangePushNotificationSettings(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (sender is ToggleSwitch ts)
            {
                await ApplicationData.Current.LocalSettings.SaveAsync(nameof(PushNotificationsEnabled), ts.IsOn);
                if (ts.IsOn)
                    PushNotificationsEnabled = await _pushNotificationService.EnablePushNotifications();
                else
                    await _pushNotificationService.DisablePushNotificaitons();
            }
        }
    }
}
