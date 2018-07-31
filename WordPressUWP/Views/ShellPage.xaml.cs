using GalaSoft.MvvmLight.Messaging;
using System;
using System.Diagnostics;
using Windows.UI.Xaml;
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
            Messenger.Default.Register<NotificationMessage>(this, (message) => GlobalInAppNotification.Show(message.Notification, 3000));
            Window.Current.SizeChanged += Current_SizeChanged;
            InitLoginPopup();
        }

        private void Current_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            InitLoginPopup();
        }

        private void InitLoginPopup()
        {
            var windowWidth = Window.Current.Bounds.Width;
            var windowHeight = Window.Current.Bounds.Height;
            double popupWidth;
            double popupHeight;

            popupHeight = windowHeight;
            popupWidth = windowWidth;

            if (windowWidth <= 700)
            {
                LoginPopupGrid.Width = windowWidth;
                LoginPopupGrid.Height = windowHeight;
            } else
            {
                // set to Auto
                LoginPopupGrid.Width = double.NaN;
                LoginPopupGrid.Height = double.NaN;
            }

            LoginPopupWrapper.Width = popupWidth;
            LoginPopupWrapper.Height = popupHeight;

            LoginPopupGrid.VerticalAlignment = VerticalAlignment.Center;
            LoginPopupGrid.HorizontalAlignment = HorizontalAlignment.Center;
        }

        private void LoginBtn_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Login();
        }

        private void Login()
        {
            ViewModel.Login(UsernameTbx.Text, PasswordTbx.Password);
            PasswordTbx.Password = String.Empty;
        }

        private void PasswordTbx_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if(e.Key == Windows.System.VirtualKey.Enter)
                Login();
        }
    }
}
