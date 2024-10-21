using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Application.CQ.Properties.Queries.RetrieveAll;
using Assembly.Horizon.Domain.Core.Uow;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Assembly.Horizon.Application.CQ.Contracts.Queries.Retrieve;

public class RetrieveContractQueryHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : IRequestHandler<RetrieveContractQuery, Result<RetrieveContractResponse, Success, Error>>
{
    public async Task<Result<RetrieveContractResponse, Success, Error>> Handle(RetrieveContractQuery request, CancellationToken cancellationToken)
    {

        var baseUrl = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";


        // Recupera o contrato
        var contract = await unitOfWork.ContractRepository.RetrieveAsync(request.Id, cancellationToken);
        if (contract == null)
        {
            return Error.NotFound; // Retorna erro se o contrato não for encontrado
        }

        // Recupera o cliente usando o CustomerId do contrato
        var customer = await unitOfWork.CustomerRepository.RetrieveAsync(contract.CustomerId, cancellationToken);
        if (customer == null)
        {
            return Error.NotFound; // Retorna erro se o cliente não for encontrado
        }

        // Recupera o corretor usando o RealtorId do contrato
        var realtor = await unitOfWork.RealtorRepository.RetrieveAsync(contract.RealtorId, cancellationToken);
        if (realtor == null)
        {
            return Error.NotFound; // Retorna erro se o corretor não for encontrado
        }

        var property = await unitOfWork.PropertyRepository.RetrieveAsync(contract.PropertyId, cancellationToken);
        if (property == null)
        {
            return Error.NotFound;
        }

        var propertyImages = property.Images.Select(image => new PropertyImageResponse
        {
            Id = image.Id,
            FileName = image.FileName,
            ImagePath = $"{baseUrl}/uploads/{image.FileName}"
        }).ToList();

        var response = new RetrieveContractResponse
        {
            Id = contract.Id,
            PropertyId = contract.PropertyId,
            CustomerId = contract.CustomerId,
            RealtorId = contract.RealtorId,
            StartDate = contract.StartDate,
            EndDate = contract.EndDate,
            Value = contract.Value,
            AdditionalFees = contract.AdditionalFees,
            PaymentFrequency = contract.PaymentFrequency,
            RenewalOption = contract.RenewalOption,
            IsActive = contract.IsActive,
            LastActiveDate = contract.LastActiveDate,
            ContractType = contract.ContractType.ToString(),
            Status = contract.Status.ToString(),
            SignatureDate = contract.SignatureDate,
            SecurityDeposit = contract.SecurityDeposit,
            InsuranceDetails = contract.InsuranceDetails,
            Notes = contract.Notes,
            TemplateVersion = contract.TemplateVersion,
            DocumentPath = contract.DocumentPath != null ?
                $"{baseUrl}/{contract.DocumentPath.Replace("\\", "/")}" : null,

            CustomerName = $"{customer?.User.Name.FirstName} {customer?.User.Name.LastName}".Trim(),
            CustomerEmail = customer?.User.Account.Email,
            CustomerPhoto = customer.User.ImageUrl != null ? $"{baseUrl}/uploads/{customer.User.ImageUrl}" : null,
            RealtorName = $"{realtor?.User.Name.FirstName} {realtor?.User.Name.LastName}".Trim(),
            RealtorEmail = realtor?.OfficeEmail,
            RealtorPhoto = realtor.User.ImageUrl != null ? $"{baseUrl}/uploads/{realtor.User.ImageUrl}" : null,

            PropertyTitle = property.Title,
            PropertyStreet = property.Address.Street,
            PropertyCity = property.Address.City,
            PropertyState = property.Address.State,
            PropertyPostalCode = property.Address.PostalCode,
            PropertyCountry = property.Address.Country,
            PropertySize = property.Size,
            PropertyBedrooms = property.Bedrooms,
            PropertyBathrooms = property.Bathrooms,
            Images = propertyImages
        };

        return response;
    }
}
