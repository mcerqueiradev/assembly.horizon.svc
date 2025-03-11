using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Application.CQ.Contracts.Queries.Retrieve;
using Assembly.Horizon.Application.CQ.Properties.Queries.RetrieveAll;
using Assembly.Horizon.Domain.Core.Uow;
using Assembly.Horizon.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Assembly.Horizon.Application.CQ.Contracts.Queries.RetrieveAll;

public class RetrieveAllContractsQueryHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : IRequestHandler<RetrieveAllContractsQuery, Result<RetrieveAllContractsResponse, Success, Error>>
{

    public async Task<Result<RetrieveAllContractsResponse, Success, Error>> Handle(RetrieveAllContractsQuery request, CancellationToken cancellationToken)
    {
        var baseUrl = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";

        var contracts = await unitOfWork.ContractRepository.RetrieveAllAsync(cancellationToken);

        // Verifica se não há contratos
        if (contracts == null || !contracts.Any())
        {
            return new RetrieveAllContractsResponse { Contracts = Enumerable.Empty<RetrieveContractResponse>() };
        }

        var contractResponses = new List<RetrieveContractResponse>();

        // Para cada contrato, busque os detalhes do cliente e do corretor
        foreach (var contract in contracts)
        {
            // Buscando o cliente
            var customer = await unitOfWork.CustomerRepository.RetrieveAsync(contract.CustomerId, cancellationToken);
            // Buscando o corretor
            var realtor = await unitOfWork.RealtorRepository.RetrieveAsync(contract.RealtorId, cancellationToken);

            var property = await unitOfWork.PropertyRepository.RetrieveAsync(contract.PropertyId, cancellationToken);

            var propertyImages = property.Images.Select(image => new PropertyImageResponse
            {
                Id = image.Id,
                FileName = image.FileName,
                ImagePath = $"{baseUrl}/uploads/{image.FileName}"
            }).ToList();

            contractResponses.Add(new RetrieveContractResponse
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
                DocumentPath = contract.DocumentPath != null ? $"{baseUrl}/{contract.DocumentPath}" : null,
                DurationInMonths = contract.DurationInMonths,
                ContractName = contract.ContractName,
                CreatedAt = contract.CreatedAt,

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

            });
        }

        var response = new RetrieveAllContractsResponse
        {
            Contracts = contractResponses
        };

        return response;
    }
}
