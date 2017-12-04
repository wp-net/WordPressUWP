using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Messaging;
using Windows.ApplicationModel.Activation;
using Windows.Networking.PushNotifications;
using WordPressUWP.Activation;
using WordPressUWP.ViewModels;
using Microsoft.Practices.ServiceLocation;
using Windows.Storage;
using WordPressUWP.Helpers;

namespace WordPressUWP.Services
{
    internal class HubNotificationsService : ActivationHandler<ToastNotificationActivatedEventArgs>
    {
        public async void InitializeAsync()
        {
            //// See more about adding push notifications to your Windows app at
            //// https://docs.microsoft.com/azure/app-service-mobile/app-service-mobile-windows-store-dotnet-get-started-push

            // check if push should be registered
            var pushNotificationsEnabled = await ApplicationData.Current.LocalSettings.ReadAsync<bool>("PushNotificationsEnabled");
            if (pushNotificationsEnabled)
            {

                //var result = await RegisterNotifications();
                //if (result.RegistrationId != null)
                //{
                //    // RegistrationID let you know it was successful
                //}
            }
            else
            {
                //await UnregisterNotifications();
            }

            // You can also send push notifications from Windows Developer Center targeting your app consumers
            // Documentation: https://docs.microsoft.com/windows/uwp/publish/send-push-notifications-to-your-apps-customers
        }

        protected override async Task HandleInternalAsync(ToastNotificationActivatedEventArgs args)
        {
            //// TODO WTS: Handle activation from toast notification,
            //// For more info handling activation see documentation at
            //// https://blogs.msdn.microsoft.com/tiles_and_toasts/2015/07/08/quickstart-sending-a-local-toast-notification-and-handling-activations-from-it-windows-10/
            var navigationService = ServiceLocator.Current.GetInstance<NavigationServiceEx>();

            navigationService.Navigate(typeof(NewsViewModel).FullName, args.Argument);

            await Task.CompletedTask;
        }

        private async Task<Registration> RegisterNotifications()
        {
            var hub = new NotificationHub(ApiCredentials.HubName, ApiCredentials.AccessSiganture);
            var channel = await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();
            return await hub.RegisterNativeAsync(channel.Uri);
        }

        private async Task UnregisterNotifications()
        {
            var hub = new NotificationHub(ApiCredentials.HubName, ApiCredentials.AccessSiganture);
            var channel = await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();
            await hub.UnregisterAllAsync(channel.Uri);
        }
    }
}
