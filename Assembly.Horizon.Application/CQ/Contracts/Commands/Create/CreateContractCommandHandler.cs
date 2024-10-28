using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Domain.Core.Interfaces;
using Assembly.Horizon.Domain.Core.Uow;
using Assembly.Horizon.Domain.Model;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Contracts.Commands.Create;

public class CreateContractCommandHandler(IUnitOfWork unitOfWork, IPdfGenerationService pdfGenerationService, INotificationStrategy notificationStrategy) : IRequestHandler<CreateContractCommand, Result<CreateContractResponse, Success, Error>>
{
    public async Task<Result<CreateContractResponse, Success, Error>> Handle(CreateContractCommand request, CancellationToken cancellationToken)
    {

        var (property, customer, realtor) = await LoadRelatedEntities(request, cancellationToken);
        if (property == null || customer == null || realtor == null)
        {
            return Error.NotFound;
        }

        var contract = new Contract
        {
            Id = Guid.NewGuid(),
            PropertyId = property.Id,
            CustomerId = customer.Id,
            RealtorId = realtor.Id,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Value = request.Value,
            AdditionalFees = request.AdditionalFees,
            PaymentFrequency = request.PaymentFrequency,
            RenewalOption = request.RenewalOption,
            IsActive = true,
            LastActiveDate = DateTime.UtcNow,
            ContractType = request.ContractType,
            Status = request.Status,
            SignatureDate = request.SignatureDate,
            SecurityDeposit = request.SecurityDeposit,
            InsuranceDetails = request.InsuranceDetails,
            Notes = request.Notes,
            DocumentPath = string.Empty,
        };

        contract.UpdateDates(contract.StartDate, contract.EndDate);

        contract.GenerateInvoices();

        await unitOfWork.ContractRepository.AddAsync(contract, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        string pdfPath = await pdfGenerationService.GenerateContractPdfAsync(contract, customer, realtor, property);

        contract.DocumentPath = pdfPath;
        await unitOfWork.ContractRepository.UpdateAsync(contract, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        var notification = new Notification(
            realtor.UserId,  // Sender (Realtor)
            customer.UserId, // Recipient (Customer)
            $"New contract created for property {property.Title}. Contract value: ${contract.Value}",
            NotificationType.Contract,
            NotificationPriority.High,
            contract.Id,
            "Contract"
        );

        await notificationStrategy.StorePersistentNotification(notification);
        await unitOfWork.NotificationRepository.AddAsync(notification);
        await unitOfWork.CommitAsync(cancellationToken);

        // Cria a resposta
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
            DurationInMonths = contract.DurationInMonths
        };

        return response;
    }

    private async Task<(Property, Customer, Realtor)> LoadRelatedEntities(CreateContractCommand request, CancellationToken cancellationToken)
    {
        Property property = null;
        Customer customer = null;
        Realtor realtor = null;

            property = await unitOfWork.PropertyRepository.RetrieveAsync(request.PropertyId, cancellationToken);
            if (property == null) throw new Exception("Property not found");

            customer = await unitOfWork.CustomerRepository.RetrieveByUserIdAsync(request.CustomerId, cancellationToken);
            if (customer == null) throw new Exception("Customer not found");

            realtor = await unitOfWork.RealtorRepository.RetrieveByUserIdAsync(request.RealtorId, cancellationToken);
            if (realtor == null) throw new Exception("Realtor not found");
      
        return (property, customer, realtor);
    }

}
