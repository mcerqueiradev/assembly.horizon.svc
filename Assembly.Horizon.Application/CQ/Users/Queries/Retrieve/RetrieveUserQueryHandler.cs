using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Domain.Core.Uow;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Users.Queries.Retrieve;

public class RetrieveUserQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<RetrieveUserQuery, Result<RetrieveUserResponse, Success, Error>>
{
    public async Task<Result<RetrieveUserResponse, Success, Error>> Handle(RetrieveUserQuery request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UserRepository.RetrieveAsync(request.Id);

        if (user == null)
            return Error.NotFound;

        var response = new RetrieveUserResponse
        {
            Id = user.Id,
            FirstName = user.Name.FirstName,
            LastName = user.Name.LastName,
            Email = user.Account.Email,
            Access = user.Access,
            ImageUrl = user.ImageUrl,
            PhoneNumber = user.PhoneNumber,
            DateOfBirth = user.DateOfBirth,
            IsActive = user.IsActive,
            LastActiveDate = user.LastActiveDate
        };

        return response;
    }
}