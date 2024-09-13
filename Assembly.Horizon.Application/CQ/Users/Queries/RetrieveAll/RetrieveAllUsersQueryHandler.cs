﻿using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Application.CQ.Users.Queries.Retrieve;
using Assembly.Horizon.Application.CQ.Users.Queries.RetrieveAll;
using Assembly.Horizon.Domain.Core.Uow;
using MediatR;

public class RetrieveAllUsersQueryHandler : IRequestHandler<RetrieveAllUsersQuery, Result<RetrieveAllUsersResponse, Success, Error>>
{
    private readonly IUnitOfWork _unitOfWork;

    public RetrieveAllUsersQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<RetrieveAllUsersResponse, Success, Error>> Handle(RetrieveAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _unitOfWork.UserRepository.RetrieveAllAsync();

        if (users == null || !users.Any())
            return Error.NotFound;

        var userResponses = users.Select(user => new RetrieveUserResponse
        {
            Id = user.Id,
            FirstName = user.Name.FirstName,
            LastName = user.Name.LastName,
            Email = user.Account.Email,
            Access = user.Access,
            ImageUrl = user.ImageUrl,
            PhoneNumber = user.PhoneNumber,
            DateOfBirth = user.DateOfBirth
        }).ToList();

        var response = new RetrieveAllUsersResponse { Users = userResponses };

        return response;
    }
}