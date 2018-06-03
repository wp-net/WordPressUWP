using System;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

using WordPressUWP.ViewModels;

namespace WordPressUWP.Views
{
    public sealed partial class SettingsPage : Page
    {
        private SettingsViewModel ViewModel
        {
            get { return DataContext as SettingsViewModel; }
        }
        private bool PushNotificationEnabled
        {
            get { return Config.NotificationsEnabled; }
        }

        //// TODO WTS: Change the URL for your privacy policy in the Resource File, currently set to https://YourPrivacyUrlGoesHere

        public SettingsPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel.Initialize();
            int[] sizes = { 12, 14, 16, 18, 20, 22, 24 };
            FontSizeCB.ItemsSource = sizes;
        }
    }
}
