using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Domain.Core.Uow;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Addresses.Queries.Retrieve;

public class RetrieveAddressHandler(IUnitOfWork unitOfWork) : IRequestHandler<RetrieveAddressQuery, Result<RetrieveAddressResponse, Success, Error>>
{
    public async Task<Result<RetrieveAddressResponse, Success, Error>> Handle(RetrieveAddressQuery request, CancellationToken cancellationToken)
    {

        var address = await unitOfWork.AddressRepository.RetrieveAsync(request.Id);

        if (address == null)
            return Error.NotFound;

        var response = new RetrieveAddressResponse
        {
            Id = address.Id,
            Street = address.Street,
            City = address.City,
            State = address.State,
            PostalCode = address.PostalCode,
            Country = address.Country,
            Reference = address.Reference
        };

        return response;
    }
}
