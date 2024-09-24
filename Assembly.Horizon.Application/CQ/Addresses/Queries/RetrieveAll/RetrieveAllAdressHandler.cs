using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Application.CQ.Addresses.Queries.Retrieve;
using Assembly.Horizon.Domain.Core.Uow;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Addresses.Queries.RetrieveAll;

public class RetrieveAllAdressHandler(IUnitOfWork unitOfWork) : IRequestHandler<RetrieveAllAddressQuery, Result<RetrieveAllAddressResponse, Success, Error>>
{
    public async Task<Result<RetrieveAllAddressResponse, Success, Error>> Handle(RetrieveAllAddressQuery request, CancellationToken cancellationToken)
    {
        var addresses = await unitOfWork.AddressRepository.RetrieveAllAsync();

        if (addresses == null || !addresses.Any())
            return Error.NotFound;

        var addressResponses = addresses.Select(address => new RetrieveAddressResponse
        {
            Id = address.Id,
            Street = address.Street,
            City = address.City,
            State = address.State,
            PostalCode = address.PostalCode,
            Country = address.Country,
            Reference = address.Reference
        }).ToList();

        var response = new RetrieveAllAddressResponse
        {
            AddressResponses = addressResponses
        };

        return response;
    }
}