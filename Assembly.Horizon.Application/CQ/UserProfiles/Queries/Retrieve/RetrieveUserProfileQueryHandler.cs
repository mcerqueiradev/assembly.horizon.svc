using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Domain.Core.Uow;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Assembly.Horizon.Application.CQ.UserProfiles.Queries.Retrieve;

public class RetrieveUserProfileQueryHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<RetrieveUserProfileQuery, Result<RetrieveUserProfileResponse, Success, Error>>
{
    public async Task<Result<RetrieveUserProfileResponse, Success, Error>> Handle(RetrieveUserProfileQuery request, CancellationToken cancellationToken)
    {
        var baseUrl = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";

        var profile = await unitOfWork.UserProfileRepository.RetrieveByUserIdAsync(request.Id);

        if (profile == null)
            return Error.NotFound;

        var response = new RetrieveUserProfileResponse
        {
            Id = profile.Id,
            Bio = profile.Bio,
            ProfileCoverUrl = profile.ProfileCoverUrl != null ? $"{baseUrl}/uploads/{profile.ProfileCoverUrl}" : null,
            Location = profile.Location,
            Website = profile.Website,
            Occupation = profile.Occupation
        };

        return response;
    }
}
