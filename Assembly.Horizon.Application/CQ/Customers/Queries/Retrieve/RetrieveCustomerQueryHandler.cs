using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Domain.Core.Uow;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Customers.Queries.Retrieve;

public class RetrieveCustomerQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<RetrieveCustomerQuery, Result<RetrieveCustomerResponse, Success, Error>>
{
    public async Task<Result<RetrieveCustomerResponse, Success, Error>> Handle(RetrieveCustomerQuery request, CancellationToken cancellationToken)
    {
        var customer = await unitOfWork.CustomerRepository.RetrieveAsync(request.Id);
        
        if (customer == null)
        {
            return Error.NotFound;
        }

        var response = new RetrieveCustomerResponse
        {
            Id = customer.Id,
            UserId = customer.UserId 
        };

        return response;
    }
}
