using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Domain.Core.Uow;
using Assembly.Horizon.Domain.Model;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Addresses.Commands.Create;

public class CreateAddressCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateAddressCommand, Result<CreateAddressResponse, Success, Error>>
{

    public async Task<Result<CreateAddressResponse, Success, Error>> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
    {

        var newAddress = new Address { 
            Street = request.Street,
            City = request.City,
            State = request.State,
            PostalCode = request.PostalCode,
            Country = request.Country,
            Reference = request.Reference
        };
        
        await unitOfWork.AddressRepository.AddAsync(newAddress, cancellationToken);
        await unitOfWork.CommitAsync();
        return Success.Ok;
    }
}