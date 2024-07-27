using TaskManagerSample.Core.Notifications;

namespace TaskManagerSample.Core.Intefaces;

public interface INotifier
{
    bool HasNotification();

    List<Notification> GetNotifications();

    void Handle(Notification notification);
}