using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Domain.Core.Interfaces;
using Assembly.Horizon.Domain.Core.Uow;
using Assembly.Horizon.Domain.Model;
using Assembly.Horizon.Infra.Data.Infrastructure.Models;
using MediatR;

namespace Assembly.Horizon.Application.CQ.PropertyProposal.Commands.Accept;

public class AcceptProposalCommand : IRequest<Result<AcceptProposalResponse, Success, Error>>
{
    public Guid ProposalId { get; set; }
}

public class AcceptProposalCommandHandler(
    IUnitOfWork unitOfWork,
    INotificationStrategy notificationStrategy,
    IEmailService emailService) : IRequestHandler<AcceptProposalCommand, Result<AcceptProposalResponse, Success, Error>>
{
    public async Task<Result<AcceptProposalResponse, Success, Error>> Handle(
        AcceptProposalCommand request,
        CancellationToken cancellationToken)
    {
        var proposal = await unitOfWork.PropertyProposalRepository.RetrieveAsync(request.ProposalId);

        if (proposal == null)
        {
            return Error.NotFound;
        }

        proposal.ApproveProposal();
        await unitOfWork.PropertyProposalRepository.UpdateAsync(proposal);

        var notification = new Notification(
            proposal.Property.Realtor.UserId,
            proposal.UserId,
            $"Proposal {proposal.ProposalNumber} has been accepted",
            NotificationType.ProposalAccepted,
            NotificationPriority.High,
            proposal.Id,
            "Proposal"
        );

        await notificationStrategy.StorePersistentNotification(notification);
        await unitOfWork.CommitAsync(cancellationToken);

        var response = new AcceptProposalResponse
        {
            ProposalId = proposal.Id,
            Status = proposal.Status,
            UpdatedAt = proposal.UpdateAt
        };

        return response;
    }
}

public class AcceptProposalResponse
{
    public Guid ProposalId { get; set; }
    public ProposalStatus Status { get; set; }
    public DateTime UpdatedAt { get; set; }
}