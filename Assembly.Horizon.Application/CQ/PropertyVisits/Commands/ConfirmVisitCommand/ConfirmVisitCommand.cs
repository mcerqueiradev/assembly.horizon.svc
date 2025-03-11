using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Domain.Core.Interfaces;
using Assembly.Horizon.Domain.Core.Uow;
using Assembly.Horizon.Domain.Model;
using Assembly.Horizon.Infra.Data.Infrastructure.Models;
using MediatR;

namespace Assembly.Horizon.Application.CQ.PropertyVisits.Commands.ConfirmVisitCommand;

public record ConfirmVisitCommand(string Token) : IRequest<Result<ConfirmVisitResponse, Success, Error>>;

public record ConfirmVisitResponse(Guid Id, VisitStatus Status);

public class ConfirmVisitCommandHandler(IUnitOfWork unitOfWork, INotificationStrategy notificationStrategy, IEmailService emailService) : IRequestHandler<ConfirmVisitCommand, Result<ConfirmVisitResponse, Success, Error>>
{
 
    public async Task<Result<ConfirmVisitResponse, Success, Error>> Handle(
        ConfirmVisitCommand request,
        CancellationToken cancellationToken)
    {
        var visit = await unitOfWork.PropertyVisitRepository.RetrieveByTokenAsync(request.Token);
        if (visit == null)
            return Error.NotFound;

        visit.Confirm();

        // Notify the user who requested the visit
        var notification = new Notification(
            visit.RealtorUserId,
            visit.UserId, // Send to the user who requested
            $"Your visit request for {visit.Property.Title} on {visit.VisitDate:d} at {visit.TimeSlot} has been confirmed by the realtor",
            NotificationType.Visit,
            NotificationPriority.High,
            visit.PropertyId,
            "Property"
        );

        // Send confirmation email to the user
        var confirmationEmail = new VisitConfirmedEmail
        {
            UserName = visit.User.Name.FirstName,
            UserEmail = visit.User.Account.Email,  // Added this line
            PropertyTitle = visit.Property.Title,
            VisitDate = visit.VisitDate,
            TimeSlot = visit.TimeSlot.ToString(),
            RealtorName = visit.RealtorUser.Name.FirstName,
            RealtorEmail = visit.RealtorUser.Account.Email,
            RealtorPhone = visit.RealtorUser.PhoneNumber
        };

        await emailService.SendVisitConfirmedEmailAsync(confirmationEmail);
        await notificationStrategy.StorePersistentNotification(notification);
        await unitOfWork.CommitAsync(cancellationToken);

        return new ConfirmVisitResponse(visit.Id, visit.VisitStatus);
    }
}
