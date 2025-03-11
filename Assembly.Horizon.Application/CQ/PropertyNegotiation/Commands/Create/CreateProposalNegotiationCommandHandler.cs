using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Domain.Core.Interfaces;
using Assembly.Horizon.Domain.Core.Uow;
using Assembly.Horizon.Domain.Model;
using Assembly.Horizon.Infra.Data.Infrastructure.Models;
using MediatR;

namespace Assembly.Horizon.Application.CQ.PropertyNegotiation.Commands.Create;

public class CreateProposalNegotiationCommandHandler(
    IUnitOfWork unitOfWork,
    INotificationStrategy notificationStrategy,
    IFileStorageService fileStorageService,
    IEmailService emailService) : IRequestHandler<CreateProposalNegotiationCommand, Result<CreateProposalNegotiationResponse, Success, Error>>
{
    public async Task<Result<CreateProposalNegotiationResponse, Success, Error>> Handle(
        CreateProposalNegotiationCommand request,
        CancellationToken cancellationToken)
    {
            var negotiation = new ProposalNegotiation
            {
                ProposalId = request.ProposalId,
                SenderId = request.SenderId,
                Message = request.Message,
                Status = NegotiationStatus.Sent
            };

            if (request.CounterOffer.HasValue)
            {
                negotiation.AddCounterOffer(request.CounterOffer.Value);
            }

            if (request.Documents != null && request.Documents.Any())
            {
                foreach (var (document, documentType) in request.Documents.Zip(request.DocumentTypes))
                {
                    var fileName = await fileStorageService.SaveFileAsync(document, cancellationToken);
                    negotiation.AttachDocument(document.FileName, fileName, documentType, document.ContentType);
                }
            }


            await unitOfWork.ProposalNegotiationRepository.AddAsync(negotiation);

            var proposal = await unitOfWork.PropertyProposalRepository.RetrieveAsync(request.ProposalId);
            var sender = await unitOfWork.UserRepository.RetrieveAsync(request.SenderId);
            var receiver = await unitOfWork.RealtorRepository.RetrieveAsync(proposal.Property.RealtorId);


            var notification = new Notification(
                sender.Id,
                receiver.UserId,
                $"New negotiation message for proposal {proposal.ProposalNumber}",
                NotificationType.Negotiation,
                NotificationPriority.High,
                proposal.Id,
                "Proposal"
            );

            await notificationStrategy.StorePersistentNotification(notification);
            await unitOfWork.CommitAsync(cancellationToken);

            var response = new CreateProposalNegotiationResponse
            {
                Id = negotiation.Id,
                Status = negotiation.Status,
                CreatedAt = negotiation.CreatedAt
            };

            return response;
    }
}
