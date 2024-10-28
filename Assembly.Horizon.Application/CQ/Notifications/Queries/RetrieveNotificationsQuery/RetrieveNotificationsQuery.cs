using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Domain.Core.Uow;
using Assembly.Horizon.Domain.Model;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Notifications.Queries.RetrieveNotificationsQuery;

public record GetNotificationsQuery(Guid UserId) : IRequest<Result<List<NotificationResponse>, Success, Error>>;


public class GetNotificationsQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetNotificationsQuery, Result<List<NotificationResponse>, Success, Error>>
{
    public async Task<Result<List<NotificationResponse>, Success, Error>> Handle(
        GetNotificationsQuery request,
        CancellationToken cancellationToken)
    {
        var notifications = await unitOfWork.NotificationRepository.GetUnreadNotificationsAsync(request.UserId, cancellationToken);

        var response = notifications.Select(n => new NotificationResponse
        {
            Id = n.Id,
            Message = n.Message,
            CreatedAt = n.CreatedAt,
            Type = n.Type.ToString(),
            Priority = n.Priority.ToString(),
            ReferenceId = n.ReferenceId,
            ReferenceType = n.ReferenceType,
            Status = n.Status.ToString(),
        }).ToList();

        return response;
    }
}

public class NotificationResponse
{
    public Guid Id { get; set; }
    public string Message { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Type { get; set; }
    public string Priority { get; set; }
    public Guid? ReferenceId { get; set; }
    public string ReferenceType { get; set; }
    public string Status { get; set; }
}