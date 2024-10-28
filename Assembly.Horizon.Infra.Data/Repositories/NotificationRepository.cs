using Assembly.Horizon.Domain.Core.Interfaces;
using Assembly.Horizon.Domain.Model;
using Assembly.Horizon.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Assembly.Horizon.Infra.Data.Repositories;

public class NotificationRepository : PaginatedRepository<Notification, Guid>, INotificationRepository
{
protected readonly ApplicationDbContext _context;
protected readonly DbSet<Notification> _dbSet;

public NotificationRepository(ApplicationDbContext context) : base(context)
{
    _context = context;
    _dbSet = context.Set<Notification>();
}

    public async Task<IEnumerable<Notification>> GetNotificationsAsync(Guid userId)
    {
        return await DbSet
            .Where(n => n.RecipientId == userId)
            .OrderByDescending(n => n.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<Notification>> GetRecentNotificationsAsync(Guid userId)
    {
        return await DbSet
            .Where(n => n.RecipientId == userId)
            .OrderByDescending(n => n.CreatedAt)
            .Take(10)
            .ToListAsync();
    }

    public async Task<IEnumerable<Notification>> GetUnreadNotificationsAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await DbSet
            .Where(n => n.RecipientId == userId && n.Status == NotificationStatus.Unread)
            .OrderByDescending(n => n.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task MarkAsReadAsync(Guid notificationId, CancellationToken cancellationToken)
    {
        var notification = await DbSet.FindAsync(new object[] { notificationId }, cancellationToken);
        if (notification != null)
        {
            notification.MarkAsRead();
            _context.Entry(notification).State = EntityState.Modified;
        }
    }
}
