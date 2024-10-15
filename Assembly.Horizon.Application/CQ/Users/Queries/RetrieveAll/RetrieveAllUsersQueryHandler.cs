using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Application.CQ.Users.Queries.Retrieve;
using Assembly.Horizon.Application.CQ.Users.Queries.RetrieveAll;
using Assembly.Horizon.Domain.Core.Uow;
using MediatR;
using Microsoft.AspNetCore.Http;

public class RetrieveAllUsersQueryHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : IRequestHandler<RetrieveAllUsersQuery, Result<RetrieveAllUsersResponse, Success, Error>>
{

    public async Task<Result<RetrieveAllUsersResponse, Success, Error>> Handle(RetrieveAllUsersQuery request, CancellationToken cancellationToken)
    {
        var baseUrl = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";

        var users = await unitOfWork.UserRepository.RetrieveAllAsync();

        if (users == null || !users.Any())
            return Error.NotFound;

        var userResponses = users.Select(user => new RetrieveUserResponse
        {
            Id = user.Id,
            FirstName = user.Name.FirstName,
            LastName = user.Name.LastName,
            Email = user.Account.Email,
            Access = user.Access.ToString(),
            ImageUrl = user.ImageUrl != null ? $"{baseUrl}/uploads/{user.ImageUrl}" : null,
            PhoneNumber = user.PhoneNumber,
            DateOfBirth = user.DateOfBirth,
            IsActive = user.IsActive,
            LastActiveDate = user.LastActiveDate,
        }).ToList();

        var response = new RetrieveAllUsersResponse { Users = userResponses };

        return response;
    }
}