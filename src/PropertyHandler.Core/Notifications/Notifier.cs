using PropertyHandler.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PropertyHandler.Core.Notifications
{
    public class Notifier : INotifier
    {
        private List<Notification> notifications;

        public Notifier() => notifications = new List<Notification>();

        public List<Notification> GetNotifications() => notifications;
        public void Handle(Notification notificacao) => notifications.Add(notificacao);
        public bool HasNotifications() => notifications.Any();
  
    }
}
