using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Domain.Core.Interfaces;
using Assembly.Horizon.Domain.Core.Uow;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.Horizon.Application.CQ.Notifications.Queries.GetRecentNotificationsQuery;

public record GetRecentNotificationsQuery(Guid UserId) : IRequest<Result<GetRecentNotificationsResponse, Success, Error>>;

public class GetRecentNotificationsQueryHandler(IUnitOfWork unitOfWork, INotificationStrategy notificationStrategy) : IRequestHandler<GetRecentNotificationsQuery, Result<GetRecentNotificationsResponse, Success, Error>>
{

    public async Task<Result<GetRecentNotificationsResponse, Success, Error>> Handle(GetRecentNotificationsQuery request, CancellationToken cancellationToken)
    {
        var notifications = await notificationStrategy.GetRecentNotifications(request.UserId);

        var notificationDtos = notifications.Select(n => new NotificationDto
        {
            Id = n.Id,
            SenderId = n.SenderId,
            RecipientId = n.RecipientId,
            Message = n.Message,
            Type = n.Type.ToString(),
            Priority = n.Priority.ToString(),
            ReferenceId = n.ReferenceId,
            ReferenceType = n.ReferenceType,
            CreatedAt = n.CreatedAt,
            Status = n.Status.ToString(),
        }).ToList();

        await unitOfWork.CommitAsync(cancellationToken);

        var response = new GetRecentNotificationsResponse
        {
            Notifications = notificationDtos
        };

        return response;
    }
}

public class GetRecentNotificationsResponse
{
    public ICollection<NotificationDto> Notifications { get; set; }
}

public class NotificationDto
{
    public Guid Id { get; set; }
    public Guid SenderId { get; set; }
    public Guid RecipientId { get; set; }
    public string Message { get; set; }
    public string Type { get; set; }
    public string Priority { get; set; }
    public Guid? ReferenceId { get; set; }
    public string ReferenceType { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Status { get; set; }
}