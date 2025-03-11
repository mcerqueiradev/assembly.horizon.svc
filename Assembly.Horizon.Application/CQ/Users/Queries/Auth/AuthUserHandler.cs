using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Domain.Core.Uow;
using Assembly.Horizon.Security.Interface;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Users.Queries.Auth;

public class AuthUserHandler : IRequestHandler<AuthUserQuery, Result<AuthUserResponse, Success, Error>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDataProtectionService _dataProtectionService;
    private readonly ITokenService _tokenService;

    public AuthUserHandler(IUnitOfWork unitOfWork,
        IDataProtectionService dataProtectionService,
        ITokenService tokenService)
    {
        _unitOfWork = unitOfWork;
        _dataProtectionService = dataProtectionService;
        _tokenService = tokenService;
    }

    public async Task<Result<AuthUserResponse, Success, Error>> Handle(AuthUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetByEmailAsync(request.Email, cancellationToken);
        if (user == null)
        {
            return Error.UserNotFound;
        }

        var hashedPassword = _dataProtectionService.GetComputedHash(request.Password, user.Account.PasswordSalt);

        if (!hashedPassword.SequenceEqual(user.Account.PasswordHash))
        {
            return Error.InvalidCredentials;
        }

        user.LastActiveDate = DateTime.UtcNow;
        await _unitOfWork.UserRepository.UpdateAsync(user, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        var token = _tokenService.GenerateToken(user);
        var response = new AuthUserResponse { 
            Token = token, 
            UserId = user.Id,
        };

        return response;
    }
}
