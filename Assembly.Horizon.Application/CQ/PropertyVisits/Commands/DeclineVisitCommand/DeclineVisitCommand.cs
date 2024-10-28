using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Domain.Core.Interfaces;
using Assembly.Horizon.Domain.Core.Uow;
using Assembly.Horizon.Domain.Model;
using Assembly.Horizon.Infra.Data.Infrastructure.Models;
using MediatR;

namespace Assembly.Horizon.Application.CQ.PropertyVisits.Commands.DeclineVisitCommand;

public record DeclineVisitCommand(string Token) : IRequest<Result<DeclineVisitResponse, Success, Error>>;

public record DeclineVisitResponse(Guid Id, VisitStatus Status);

public class DeclineVisitCommandHandler(IUnitOfWork unitOfWork, INotificationStrategy notificationStrategy, IEmailService emailService): IRequestHandler<DeclineVisitCommand, Result<DeclineVisitResponse, Success, Error>>
{
    public async Task<Result<DeclineVisitResponse, Success, Error>> Handle(
        DeclineVisitCommand request,
        CancellationToken cancellationToken)
    {
        var visit = await unitOfWork.PropertyVisitRepository.RetrieveByTokenAsync(request.Token);
        if (visit == null)
            return Error.NotFound;

        visit.Cancel();

        var notification = new Notification(
            visit.UserId,
            visit.RealtorUserId,
            $"Visit for {visit.Property.Title} has been declined for {visit.VisitDate:d} at {visit.TimeSlot}",
            NotificationType.Visit,
            NotificationPriority.High,
            visit.PropertyId,
            "Property"
        );

        await notificationStrategy.StorePersistentNotification(notification);
        await unitOfWork.CommitAsync(cancellationToken);

        return new DeclineVisitResponse(visit.Id, visit.VisitStatus);
    }
}
