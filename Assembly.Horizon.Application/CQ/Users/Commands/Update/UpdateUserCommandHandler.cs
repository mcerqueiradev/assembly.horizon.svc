using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Domain.Core.Uow;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Users.Commands.Update;

public class UpdateUserCommandHadler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateUserCommand, Result<UpdateUserResponse, Success, Error>>
{
    public async Task<Result<UpdateUserResponse, Success, Error>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await unitOfWork.UserRepository.RetrieveAsync(request.Id);

        if (existingUser == null)
            return Error.NotFound;

        existingUser.Name.FirstName = request.FirstName;
        existingUser.Name.LastName = request.LastName;
        existingUser.ImageUrl = request.ImageUrl;
        existingUser.Access = request.Access;
        existingUser.PhoneNumber = request.PhoneNumber;
        existingUser.DateOfBirth = request.DateOfBirth;
        existingUser.IsActive = request.IsActive;
        existingUser.LastActiveDate = request.LastActiveDate;

        var account = await unitOfWork.AccountRepository.RetrieveAsync(existingUser.Account.Id);
        if (account == null)
            return Error.NotFound;

        if (!string.IsNullOrEmpty(request.Email) && request.Email != account.Email)
        {
            account.Email = request.Email;
        }

        if (!string.IsNullOrEmpty(request.Password))
        {
            var protectionKeys = unitOfWork.DataProtectionService.Protect(request.Password);
            account.PasswordHash = protectionKeys.PasswordHash;
            account.PasswordSalt = protectionKeys.PasswordSalt;
        }

        unitOfWork.UserRepository.UpdateAsync(existingUser);

        unitOfWork.AccountRepository.UpdateAsync(account);

        await unitOfWork.CommitAsync();

        var token = unitOfWork.TokenService.GenerateToken(existingUser);

        return Success.Ok;
    }
}