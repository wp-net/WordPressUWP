using Microsoft.WindowsAzure.Messaging;
using System;
using System.Threading.Tasks;
using Windows.Networking.PushNotifications;
using WordPressUWP.Helpers;
using WordPressUWP.Interfaces;

namespace WordPressUWP.Services
{
    public class PushNotificationService : IPushNotificationService
    {
        public async Task DisablePushNotificaitons()
        {
            try
            {
                var hub = new NotificationHub(Config.HubName, Config.AccessSiganture);
                var channel = await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();
                await hub.UnregisterAllAsync(channel.Uri);
            }
            catch
            {
                // error removing subcription
            }
            
        }

        public async Task<bool> EnablePushNotifications()
        {
            try
            {
                var hub = new NotificationHub(Config.HubName, Config.AccessSiganture);
                var channel = await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();
                var result = await hub.RegisterNativeAsync(channel.Uri);
                return result.RegistrationId != null;
            }
            catch
            {
                // error subscribing to push notifications
                return false;
            }

        }
    }
}
