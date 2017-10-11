using System;
using WordPressUWP.Interfaces;

namespace WordPressUWP.Services
{
    public class InAppNotificationService : IInAppNotificationService
    {
        public event EventHandler<string> InAppNotificationRaised;


        public void RaiseInAppNotification(string text)
        {
            InAppNotificationRaised.Invoke(null, text);
        }
    }
}
