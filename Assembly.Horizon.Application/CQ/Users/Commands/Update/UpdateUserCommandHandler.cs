using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Domain.Core.Interfaces;
using Assembly.Horizon.Domain.Core.Uow;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Users.Commands.Update;

public class UpdateUserCommandHadler(IUnitOfWork unitOfWork, IFileStorageService fileStorageService) : IRequestHandler<UpdateUserCommand, Result<UpdateUserResponse, Success, Error>>
{
    public async Task<Result<UpdateUserResponse, Success, Error>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
            var existingUser = await unitOfWork.UserRepository.RetrieveAsync(request.Id);

            if (existingUser == null)
                return Error.NotFound;

            existingUser.Name.FirstName = request.FirstName;
            existingUser.Name.LastName = request.LastName;
            existingUser.PhoneNumber = request.PhoneNumber ?? null;

            existingUser.DateOfBirth = request.DateOfBirth;

            existingUser.IsActive = true;
            existingUser.LastActiveDate = DateTime.UtcNow;

            if (request.Upload != null)
            {
                var fileName = await fileStorageService.SaveFileAsync(request.Upload, cancellationToken);
                existingUser.ImageUrl = fileName;
            }
            else if (!string.IsNullOrEmpty(request.ImageUrl))
            {
                var imageUrlFileName = Path.GetFileName(request.ImageUrl);
                existingUser.ImageUrl = imageUrlFileName;
            }

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