﻿using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Domain.Core.Uow;
using Assembly.Horizon.Domain.Model;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Users.Commands.Create;

public class CreateUserCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateUserCommand, Result<CreateUserResponse, Success, Error>>
{

    public async Task<Result<CreateUserResponse, Success, Error>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await unitOfWork.UserRepository.GetByEmailAsync(request.Email, cancellationToken);
        if (existingUser != null)
        {
            return Error.ExistingUser;
        }

        var protectionKeys = unitOfWork.DataProtectionService.Protect(request.Password);

        var newUser = new User
        {
            Name = new Name
            {
                FirstName = request.FirstName,
                LastName = request.LastName
            },
            ImageUrl = request.ImageUrl,
            PhoneNumber = request.PhoneNumber,
            DateOfBirth = request.DateOfBirth,
        };
        var newAccount = new Account
        {
            Email = request.Email,
            PasswordHash = protectionKeys.PasswordHash,
            PasswordSalt = protectionKeys.PasswordSalt,
            UserId = newUser.Id,
        };

        await unitOfWork.UserRepository.AddAsync(newUser, cancellationToken);
        await unitOfWork.AccountRepository.AddAsync(newAccount, cancellationToken);
        await unitOfWork.CommitAsync();

        var token = unitOfWork.TokenService.GenerateToken(newUser);

        return Success.Ok;
    }
}