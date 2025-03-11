using Assembly.Horizon.Application.Common.Responses;
using MediatR;

namespace Assembly.Horizon.Application.CQ.UserProfiles.Queries.Retrieve;

public record RetrieveUserProfileQuery(Guid Id) : IRequest<Result<RetrieveUserProfileResponse, Success, Error>>;
