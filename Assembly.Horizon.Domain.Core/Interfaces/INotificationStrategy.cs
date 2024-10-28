using Assembly.Horizon.Domain.Model;

namespace Assembly.Horizon.Domain.Core.Interfaces;

public interface INotificationStrategy
{

    Task StoreTransientNotification(Notification notification);
    Task<IEnumerable<Notification>> GetRecentNotifications(Guid userId);

    // Notificações importantes (Banco de Dados)
    Task StorePersistentNotification(Notification notification);
    Task<IEnumerable<Notification>> GetPersistentNotifications(Guid userId);
    Task UpdateNotificationStatus(Notification notification);
}
