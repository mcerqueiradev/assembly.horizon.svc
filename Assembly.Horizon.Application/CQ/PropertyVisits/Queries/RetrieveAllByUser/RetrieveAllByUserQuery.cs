using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Application.CQ.PropertyVisits.Queries.Retrieve;
using Assembly.Horizon.Application.CQ.PropertyVisits.Queries.RetrieveAll;
using Assembly.Horizon.Domain.Core.Interfaces;
using Assembly.Horizon.Domain.Core.Uow;
using Assembly.Horizon.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Assembly.Horizon.Application.CQ.PropertyVisits.Queries.RetrieveAllByUser;

public class RetrieveAllPropertyVisitsByUserQuery : IRequest<Result<RetrieveAllPropertyVisitsResponse, Success, Error>>
{
    public Guid UserId { get; set; }
}

public class RetrieveAllPropertyVisitsByUserQueryHandler(
    IUnitOfWork unitOfWork,
    INotificationStrategy notificationStrategy,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<RetrieveAllPropertyVisitsByUserQuery, Result<RetrieveAllPropertyVisitsResponse, Success, Error>>
{
    public async Task<Result<RetrieveAllPropertyVisitsResponse, Success, Error>> Handle(
        RetrieveAllPropertyVisitsByUserQuery request,
        CancellationToken cancellationToken)
    {
        var visits = await unitOfWork.PropertyVisitRepository.RetrieveAllByUserAsync(request.UserId, cancellationToken);

        if (visits == null || !visits.Any())
            return Error.NotFound;

        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var currentVisits = visits.Where(v => v.VisitDate >= today);

        var todayVisits = visits.Where(v => v.VisitDate == today);


        if (todayVisits.Any())
        {
            var recentNotifications = await notificationStrategy.GetRecentNotifications(request.UserId);

            var existingTodayNotification = recentNotifications
                    .Where(n => n.Type == NotificationType.Visit)
                    .Where(n => DateOnly.FromDateTime(n.CreatedAt) == today)
                    .Any();

            if (!existingTodayNotification)
            {
                var notification = new Notification(
                    request.UserId,
                    request.UserId,
                    $"You have {todayVisits.Count()} visits scheduled for today",
                    NotificationType.Visit,
                    NotificationPriority.High,
                    todayVisits.First().Id,
                    "Visit"
                );

                await notificationStrategy.StoreTransientNotification(notification);

            }
        }

        var visitsResponses = currentVisits.Select(visit => new RetrievePropertyVisitResponse
        {
            Id = visit.Id,
            PropertyId = visit.PropertyId,
            UserId = visit.UserId,
            RealtorId = visit.RealtorUserId,
            VisitDate = visit.VisitDate,
            TimeSlot = visit.TimeSlot,
            Status = visit.VisitStatus,
            Notes = visit.Notes,
            PropertyTitle = visit.Property.Title,
            UserName = $"{visit.User.Name.FirstName} {visit.User.Name.LastName}".Trim(),
            RealtorName = $"{visit.RealtorUser.Name.FirstName} {visit.RealtorUser.Name.LastName}".Trim(),
        }).ToList();

        var response = new RetrieveAllPropertyVisitsResponse { Visits = visitsResponses };
        return response;
    }
}