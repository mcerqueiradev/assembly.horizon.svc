using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Application.CQ.Users.Queries.Retrieve;
using Assembly.Horizon.Domain.Core.Uow;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Assembly.Horizon.Application.CQ.Users.Queries.RetrieveNonAdmins;

public class RetrieveNonAdminsQueryHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : IRequestHandler<RetrieveNonAdminsQuery, Result<RetrieveNonAdminsResponse, Success, Error>>
{

    public async Task<Result<RetrieveNonAdminsResponse, Success, Error>> Handle(RetrieveNonAdminsQuery request, CancellationToken cancellationToken)
    {
        var baseUrl = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";

        var users = await unitOfWork.UserRepository.RetrieveAllAsync();

        if (users == null || !users.Any())
            return Error.NotFound;

        var nonAdminUsers = users.Where(user => user.Access != Domain.Model.Access.SystemAdministrator && user.Access != Domain.Model.Access.LicensedAgent).ToList();

        // Verifica se existem usuários não administradores
        if (nonAdminUsers == null || !nonAdminUsers.Any())
            return Error.NotFound;

        // Mapeia os usuários para o formato de resposta
        var userResponses = nonAdminUsers.Select(user => new RetrieveUserResponse
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

        // Cria a resposta
        var response = new RetrieveNonAdminsResponse { Users = userResponses };

        return response;
    }
}
