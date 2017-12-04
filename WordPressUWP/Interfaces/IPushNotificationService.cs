using System.Threading.Tasks;

namespace WordPressUWP.Interfaces
{
    public interface IPushNotificationService
    {
        Task<bool> EnablePushNotifications();
        Task DisablePushNotificaitons();
    }
}
