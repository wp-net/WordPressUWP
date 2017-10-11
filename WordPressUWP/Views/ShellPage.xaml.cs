using System;

using Windows.UI.Xaml.Controls;
using WordPressUWP.ViewModels;

namespace WordPressUWP.Views
{
    public sealed partial class ShellPage : Page
    {
        private ShellViewModel ViewModel
        {
            get { return DataContext as ShellViewModel; }
        }

        public ShellPage()
        {
            InitializeComponent();
            DataContext = ViewModel;
            ViewModel.Initialize(shellFrame);
        }

        private void Page_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ViewModel.InAppNotificationRaised += ViewModel_InAppNotificationRaised;
        }

        private void ViewModel_InAppNotificationRaised(object sender, string e)
        {
            GlobalInAppNotification.Show(e);
        }

        private void LoginBtn_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ViewModel.Login(UsernameTbx.Text, PasswordTbx.Password);
            PasswordTbx.Password = String.Empty;
        }

    }
}
