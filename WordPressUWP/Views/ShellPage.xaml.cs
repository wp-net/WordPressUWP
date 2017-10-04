using System;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

using WordPressUWP.Services;
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

        private void LoginBtn_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ViewModel.Login(UsernameTbx.Text, PasswordTbx.Password);
            PasswordTbx.Password = String.Empty;
        }
    }
}
