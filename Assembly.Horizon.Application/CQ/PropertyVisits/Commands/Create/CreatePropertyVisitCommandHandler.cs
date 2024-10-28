using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Domain.Core.Interfaces;
using Assembly.Horizon.Domain.Core.Uow;
using Assembly.Horizon.Domain.Model;
using Assembly.Horizon.Infra.Data.Infrastructure.Models;
using MediatR;

namespace Assembly.Horizon.Application.CQ.PropertyVisits.Commands.Create;

public class CreatePropertyVisitCommandHandler(
    IUnitOfWork unitOfWork,
    INotificationStrategy notificationStrategy,
    IEmailService emailService)
    : IRequestHandler<CreatePropertyVisitCommand, Result<CreatePropertyVisitResponse, Success, Error>>
{
    public async Task<Result<CreatePropertyVisitResponse, Success, Error>> Handle(
      CreatePropertyVisitCommand request,
      CancellationToken cancellationToken)
    {
        var property = await unitOfWork.PropertyRepository.RetrieveAsync(request.PropertyId, cancellationToken);
        var user = await unitOfWork.UserRepository.RetrieveAsync(request.UserId, cancellationToken);
        var realtor = await unitOfWork.RealtorRepository.RetrieveAsync(property.RealtorId, cancellationToken);

        var propertyVisit = new PropertyVisit(
            Guid.NewGuid(),
            null,
            request.PropertyId,
            null,
            request.UserId,
            null,
            realtor.UserId,
            request.VisitDate,
            request.TimeSlot,
            VisitStatus.Scheduled
        )
        {
            Notes = request.Notes
        };

        await unitOfWork.PropertyVisitRepository.AddAsync(propertyVisit, cancellationToken);

        var emailModel = new VisitScheduledEmail
        {
            PropertyTitle = property.Title,
            VisitDate = request.VisitDate,
            TimeSlot = request.TimeSlot,
            UserName = $"{user.Name.FirstName.Trim()} {user.Name.LastName.Trim()}",
            UserEmail = user.Account.Email,
            PhoneNumber = user.PhoneNumber ?? "Not provided",
            RealtorName = $"{realtor.User.Name.FirstName.Trim()} {realtor.User.Name.LastName.Trim()}",
            RealtorEmail = realtor.User.Account.Email,
            PropertyAddress = $"{property.Address.Street}, {property.Address.City}, {property.Address.State} {property.Address.PostalCode}, {property.Address.Country}".Trim(),
            Notes = request.Notes,
            ConfirmationToken = propertyVisit.ConfirmationToken
        };

        await emailService.SendVisitScheduledEmailAsync(emailModel);

        var notification = new Notification(
            user.Id,
            realtor.UserId,
            $"New visit scheduled for {property.Title} on {request.VisitDate:d} at {request.TimeSlot}",
            NotificationType.Visit,
            NotificationPriority.High,
            property.Id,
            "Property"
        );

        await notificationStrategy.StoreTransientNotification(notification);
        await unitOfWork.CommitAsync(cancellationToken);

        return new CreatePropertyVisitResponse
        {
            Id = propertyVisit.Id,
            VisitDate = propertyVisit.VisitDate,
            TimeSlot = propertyVisit.TimeSlot,
            Status = propertyVisit.VisitStatus
        };
    }
}
