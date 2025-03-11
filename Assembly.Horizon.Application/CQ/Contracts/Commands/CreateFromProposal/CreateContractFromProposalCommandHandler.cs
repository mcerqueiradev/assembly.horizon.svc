using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Application.CQ.Contracts.Commands.Create;
using Assembly.Horizon.Domain.Core.Interfaces;
using Assembly.Horizon.Domain.Core.Uow;
using Assembly.Horizon.Domain.Model;
using Assembly.Horizon.Infra.Data.Infrastructure.Models;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Contracts.Commands.CreateFromProposal;

public class CreateContractFromProposalCommandHandler(IUnitOfWork unitOfWork, IPdfGenerationService pdfGenerationService, INotificationStrategy notificationStrategy, IEmailService emailService) : IRequestHandler<CreateContractFromProposalCommand, Result<CreateContractResponse, Success, Error>>
{
    public async Task<Result<CreateContractResponse, Success, Error>> Handle(
        CreateContractFromProposalCommand request,
        CancellationToken cancellationToken)
    {
   try
        {
            var proposal = await unitOfWork.PropertyProposalRepository.RetrieveAsync(request.ProposalId, cancellationToken);
            if (proposal == null) return Error.NotFound;

            var (property, customer, realtor) = await LoadRelatedEntities(proposal, cancellationToken);
            if (property == null || customer == null || realtor == null)
                return Error.NotFound;

            var contract = new Contract
            {
                Id = Guid.NewGuid(),
                PropertyId = proposal.PropertyId,
                Customer = customer,
                CustomerId = customer.Id,
                RealtorId = realtor.Id,
                StartDate = proposal.IntendedMoveDate,
                EndDate = request.EndDate,
                Value = proposal.ProposedValue,
                AdditionalFees = request.AdditionalFees,
                PaymentFrequency = request.PaymentFrequency,
                RenewalOption = request.RenewalOption,
                IsActive = true,
                LastActiveDate = DateTime.UtcNow,
                ContractType = request.ContractType,
                Status = request.Status,
                SignatureDate = DateTime.UtcNow,
                SecurityDeposit = request.SecurityDeposit,
                InsuranceDetails = request.InsuranceDetails,
                Notes = request.Notes,
                DocumentPath = string.Empty,
                ProposalId = proposal.Id
            };

            contract.UpdateDates(contract.StartDate, contract.EndDate);
            contract.GenerateTransactions();

            await unitOfWork.ContractRepository.AddAsync(contract, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);

            string pdfPath = await pdfGenerationService.GenerateContractPdfAsync(contract, customer, realtor, property);
            contract.DocumentPath = pdfPath;

            await unitOfWork.ContractRepository.UpdateAsync(contract, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);


            var emailModel = new ContractSubmittedEmail
            {
                PropertyTitle = property.Title,
                ContractValue = contract.Value,
                PaymentFrequency = contract.PaymentFrequency,
                StartDate = contract.StartDate,
                EndDate = contract.EndDate,
                ContractType = contract.ContractType.ToString(),
                CustomerName = $"{customer.User.Name.FirstName.Trim()} {customer.User.Name.LastName.Trim()}",
                CustomerEmail = customer.User.Account.Email,
                RealtorName = $"{realtor.User.Name.FirstName.Trim()} {realtor.User.Name.LastName.Trim()}",
                RealtorEmail = realtor.User.Account.Email,
                PropertyAddress = $"{property.Address.Street}, {property.Address.City}, {property.Address.State} {property.Address.PostalCode}, {property.Address.Country}".Trim(),
                SecurityDeposit = contract.SecurityDeposit ?? 0,
                DocumentPath = contract.DocumentPath
            };

            await emailService.SendContractSubmittedEmailAsync(emailModel);


            var notification = new Notification(
                    realtor.UserId,
                    customer.UserId,
                    $"New contract created from proposal {proposal.ProposalNumber}. Contract value: ${contract.Value}",
                    NotificationType.Contract,
                    NotificationPriority.High,
                    contract.Id,
                    "Contract"
                );

            await notificationStrategy.StorePersistentNotification(notification);
            await unitOfWork.NotificationRepository.AddAsync(notification);
            await unitOfWork.CommitAsync(cancellationToken);

            await unitOfWork.PropertyProposalRepository.UpdateStatusAsync(
                proposal.Id,
                ProposalStatus.Completed,
                cancellationToken
            );
            await unitOfWork.CommitAsync(cancellationToken);

            var response = new CreateContractResponse
            {
                Id = contract.Id,
                PropertyId = contract.PropertyId,
                CustomerId = contract.CustomerId,
                RealtorId = contract.RealtorId,
                StartDate = contract.StartDate,
                EndDate = contract.EndDate,
                Value = contract.Value,
                IsActive = contract.IsActive,
                ContractType = contract.ContractType,
                Status = contract.Status,
                SignatureDate = contract.SignatureDate,
                DocumentPath = contract.DocumentPath,
                DurationInMonths = contract.DurationInMonths,
                ProposalId = contract.ProposalId
            };

            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine( ex.ToString() );
           return  Error.NotFound;
        }
    }

    private async Task<(Property, Customer, Realtor)> LoadRelatedEntities(
        Domain.Model.PropertyProposal proposal,
        CancellationToken cancellationToken)
    {
        var property = await unitOfWork.PropertyRepository.RetrieveAsync(proposal.PropertyId, cancellationToken);
        if (property == null) throw new Exception("Property not found");

        var customer = await unitOfWork.CustomerRepository.RetrieveByUserIdAsync(proposal.UserId, cancellationToken);
        var userId= customer.UserId;
        if (customer == null) throw new Exception("Customer not found");

        var realtor = await unitOfWork.RealtorRepository.RetrieveAsync(property.RealtorId, cancellationToken);
        if (realtor == null) throw new Exception("Realtor not found");

        return (property, customer, realtor);
    }

}
