using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Domain.Core.Interfaces;
using Assembly.Horizon.Domain.Core.Uow;
using Assembly.Horizon.Domain.Model;
using Assembly.Horizon.Infra.Data.Infrastructure.Models;
using MediatR;

namespace Assembly.Horizon.Application.CQ.PropertyNegotiation.Commands.AcceptNegotiation;

public class AcceptNegotiationCommand : IRequest<Result<AcceptNegotiationResponse, Success, Error>>
{
    public Guid ProposalId { get; set; }
    public Guid NegotiationId { get; set; }
}

public class AcceptNegotiationCommandHandler(
    IUnitOfWork unitOfWork,
    INotificationStrategy notificationStrategy,
    IEmailService emailService) : IRequestHandler<AcceptNegotiationCommand, Result<AcceptNegotiationResponse, Success, Error>>
{
    public async Task<Result<AcceptNegotiationResponse, Success, Error>> Handle(
        AcceptNegotiationCommand request,
        CancellationToken cancellationToken)
    {
            var negotiation = await unitOfWork.ProposalNegotiationRepository.RetrieveAsync(request.NegotiationId);

            if (negotiation == null)
            {
                return Error.NotFound;
            }

            var proposal = await unitOfWork.PropertyProposalRepository.RetrieveAsync(request.ProposalId);
            var senderId = negotiation.SenderId;

            Guid receiverId;
            if (proposal.Property.Realtor.UserId == senderId)
            {
                receiverId = proposal.UserId;
            }
            else
            {
                receiverId = proposal.Property.Realtor.UserId;
            }

            negotiation.Accept();
            proposal.ApproveProposal();

            await unitOfWork.ProposalNegotiationRepository.UpdateAsync(negotiation);
            await unitOfWork.PropertyProposalRepository.UpdateAsync(proposal);


            var notification = new Notification(
                    senderId,
                    receiverId,
                    $"Proposal {proposal.ProposalNumber} has been accepted",
                    NotificationType.ProposalAccepted,
                    NotificationPriority.High,
                    proposal.Id,
                    "Proposal"
                );

            await notificationStrategy.StorePersistentNotification(notification);
            await unitOfWork.CommitAsync(cancellationToken);

            var response = new AcceptNegotiationResponse
            {
                NegotiationId = negotiation.Id,
                Status = negotiation.Status,
                UpdatedAt = negotiation.UpdateAt
            };

            return response;
    }
}

public class AcceptNegotiationResponse
{
    public Guid NegotiationId { get; set; }
    public NegotiationStatus Status { get; set; }
    public DateTime UpdatedAt { get; set; }
}

