using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Application.CQ.Properties.Queries.RetrieveAll;
using Assembly.Horizon.Domain.Core.Uow;
using Assembly.Horizon.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Assembly.Horizon.Application.CQ.Invoices.Queries.Retrieve;

public record RetrieveInvoiceQuery(Guid Id) : IRequest<Result<InvoiceResponse, Success, Error>>;


public record InvoiceResponse(
    Guid Id,
    Guid ContractId,
    string InvoiceNumber,
    decimal Amount,
    DateTime DueDate,
    string Status,
    DateTime CreatedAt,
    // Contract related information
    string PropertyName,
    string PropertyAddress,
    decimal ContractAmount,
    DateTime ContractStartDate,
    DateTime ContractEndDate,
    string ContractType,
    string PaymentFrequency,
    int DurationInMonths,
    decimal AdditionalFees,
    decimal? SecurityDeposit,
    List<PropertyImageResponse> Images,
    string CustomerName,
    string CustomerEmail,
    string CustomerPhoto,
    string RealtorName,
    string RealtorEmail,
    string RealtorPhoto,
    string TransactionNumber
);

public class RetrieveInvoiceQueryHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : IRequestHandler<RetrieveInvoiceQuery, Result<InvoiceResponse, Success, Error>>
{
    public async Task<Result<InvoiceResponse, Success, Error>> Handle(
        RetrieveInvoiceQuery request,
        CancellationToken cancellationToken)
    {
        var baseUrl = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";

        var invoice = await unitOfWork.InvoiceRepository.RetrieveAsync(request.Id);
        if (invoice is null)
            return Error.NotFound;

        var contract = await unitOfWork.ContractRepository.RetrieveAsync(invoice.ContractId);
        if (contract is null)
            return Error.NotFound;

        var formattedAddress = $"{contract.Property.Address.Street}, {contract.Property.Address.City} {contract.Property.Address.PostalCode}, {contract.Property.Address.Country}";
        var propertyImages = contract.Property.Images.Select(image => new PropertyImageResponse
        {
            Id = image.Id,
            FileName = image.FileName,
            ImagePath = $"{baseUrl}/uploads/{image.FileName}"
        }).ToList();

        var customerFullName = $"{contract.Customer.User.Name.FirstName} {contract.Customer.User.Name.LastName}".Trim();
        var realtorFullName = $"{contract.Realtor.User.Name.FirstName} {contract.Realtor.User.Name.LastName}".Trim();
        var customerImageUrl = contract.Customer.User.ImageUrl != null ? $"{baseUrl}/uploads/{contract.Customer.User.ImageUrl}" : null;
        var realtorImageUrl = contract.Realtor.User.ImageUrl != null ? $"{baseUrl}/uploads/{contract.Realtor.User.ImageUrl}" : null;

        var transaction = await unitOfWork.TransactionRepository.RetrieveByInvoiceIdAsync(invoice.Id);

        var transactionNumber = transaction?.TransactionNumber;

        var response = new InvoiceResponse(
            invoice.Id,
            invoice.ContractId,
            invoice.InvoiceNumber,
            invoice.Amount,
            invoice.DueDate,
            invoice.Status.ToString(),
            invoice.CreatedAt,
            contract.Property.Title,
            formattedAddress,
            contract.Value,
            contract.StartDate,
            contract.EndDate,
            contract.ContractType.ToString(),
            contract.PaymentFrequency.ToString(),
            contract.DurationInMonths,
            contract.AdditionalFees,
            contract.SecurityDeposit,
            propertyImages,
            customerFullName,
            contract.Customer.User.Account.Email,
            customerImageUrl,
            realtorFullName,
            contract.Realtor.OfficeEmail,
            realtorImageUrl,
            transactionNumber
            
        );

        return response;
    }
}
