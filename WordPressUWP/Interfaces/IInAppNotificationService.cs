using System;

namespace WordPressUWP.Interfaces
{
    public interface IInAppNotificationService
    {
        event EventHandler<string> InAppNotificationRaised;
        void RaiseInAppNotification(string text);
    }
}
