using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Domain.Core.Uow;
using Assembly.Horizon.Domain.Model;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Customers.Commands;

public class CreateCustomerCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateCustomerCommand, Result<CreateCustomerResponse, Success, Error>>
{

    public async Task<Result<CreateCustomerResponse, Success, Error>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {

        var user = await unitOfWork.UserRepository.RetrieveAsync(request.UserId, cancellationToken);
        if(user == null)
        {
            return Error.ExistingUser;
        }

        var customer = new Customer
        {
            UserId = request.UserId,
        };

        await unitOfWork.CustomerRepository.AddAsync(customer, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        var response = new CreateCustomerResponse
        {
            User = user,
            UserId = request.UserId,
        };

        return response;
    }
}
