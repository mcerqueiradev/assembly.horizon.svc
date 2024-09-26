using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Application.CQ.Customers.Queries.Retrieve;
using Assembly.Horizon.Domain.Core.Uow;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Customers.Queries.RetrieveAll;

public class RetrieveAllCustomersQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<RetrieveAllCustomersQuery, Result<RetrieveAllCustomersResponse, Success, Error>>
{
    public async Task<Result<RetrieveAllCustomersResponse, Success, Error>> Handle(RetrieveAllCustomersQuery request, CancellationToken cancellationToken)
    {
        var customers = await unitOfWork.CustomerRepository.RetrieveAllAsync();

        if (customers == null)
        {
            return Error.NotFound;
        }

        var customerResponses = customers.Select(customer => new RetrieveCustomerResponse
        {
            Id = customer.Id,
            UserId = customer.UserId,
        }).ToList();

        var response = new RetrieveAllCustomersResponse
        {
            Customers = customerResponses,
        };

        return response;
    }
}
