using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Domain.Core.Interfaces;
using Assembly.Horizon.Domain.Core.Uow;
using Assembly.Horizon.Domain.Model;
using Assembly.Horizon.Infra.Data.Infrastructure;
using Assembly.Horizon.Infra.Data.Infrastructure.Models;
using MediatR;

namespace Assembly.Horizon.Application.CQ.PropertyProposal.Commands.Create;

public class CreatePropertyProposalCommand : IRequest<Result<CreatePropertyProposalResponse, Success, Error>>
{
    public Guid PropertyId { get; set; }
    public Guid UserId { get; set; }
    public decimal ProposedValue { get; set; }
    public ProposalType Type { get; set; }
    public string PaymentMethod { get; set; }
    public DateTime IntendedMoveDate { get; set; }
}

public class CreatePropertyProposalCommandHandler(
    IUnitOfWork unitOfWork,
    INotificationStrategy notificationStrategy,
    IEmailService emailService) : IRequestHandler<CreatePropertyProposalCommand, Result<CreatePropertyProposalResponse, Success, Error>>
{
    public async Task<Result<CreatePropertyProposalResponse, Success, Error>> Handle(
        CreatePropertyProposalCommand request,
        CancellationToken cancellationToken)
    {
        var proposal = new Domain.Model.PropertyProposal(
            request.PropertyId,
            request.UserId,
            request.ProposedValue,
            request.Type,
            request.PaymentMethod,
            request.IntendedMoveDate
        );

        await unitOfWork.PropertyProposalRepository.AddAsync(proposal);

        var property = await unitOfWork.PropertyRepository.RetrieveAsync(request.PropertyId);
        var user = await unitOfWork.UserRepository.RetrieveAsync(request.UserId);
        var realtor = await unitOfWork.RealtorRepository.RetrieveAsync(property.RealtorId);

        var emailModel = new ProposalSubmittedEmail
        {
            PropertyTitle = property.Title,
            ProposedValue = request.ProposedValue,
            PaymentMethod = request.PaymentMethod,
            IntendedMoveDate = request.IntendedMoveDate,
            ProposalType = request.Type.ToString(),
            UserName = $"{user.Name.FirstName.Trim()} {user.Name.LastName.Trim()}",
            UserEmail = user.Account.Email,
            PhoneNumber = user.PhoneNumber ?? "Not provided",
            RealtorName = $"{realtor.User.Name.FirstName.Trim()} {realtor.User.Name.LastName.Trim()}",
            RealtorEmail = realtor.User.Account.Email,
            PropertyAddress = $"{property.Address.Street}, {property.Address.City}, {property.Address.State} {property.Address.PostalCode}, {property.Address.Country}".Trim()
        };

        await emailService.SendProposalSubmittedEmailAsync(emailModel);

        var notification = new Notification(
            user.Id,
            realtor.UserId,
            $"New {request.Type.ToString().ToLower()} proposal for {property.Title} - {request.ProposedValue:C}",
            NotificationType.Proposal,
            NotificationPriority.High,
            property.Id,
            "Property"
        );

        await notificationStrategy.StoreTransientNotification(notification);
        await unitOfWork.CommitAsync(cancellationToken);

        var response = new CreatePropertyProposalResponse
        {
            Id = proposal.Id,
            Status = proposal.Status,
            CreatedAt = proposal.CreatedAt
        };

        return response;
    }
}

public class CreatePropertyProposalResponse
{
    public Guid Id { get; set; }
    public ProposalStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
}
