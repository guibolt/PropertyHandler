using PropertyHandler.Core.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyHandler.Core.Interfaces
{
    public interface  INotifier 
    {
        bool HasNotifications();
        List<Notification> GetNotifications();
        void Handle(Notification notificacao);
    }
}
