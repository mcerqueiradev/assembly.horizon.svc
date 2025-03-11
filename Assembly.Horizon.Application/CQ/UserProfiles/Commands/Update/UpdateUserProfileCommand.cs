using Assembly.Horizon.Application.Common.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Assembly.Horizon.Application.CQ.UserProfiles.Commands.Update;

public class UpdateUserProfileCommand : IRequest<Result<UpdateUserProfileResponse, Success, Error>>
{
    public Guid Id { get; set; }
    public string? Bio { get; set; }
    public string? Location { get; set; }
    public string? Website { get; set; }
    public string? Occupation { get; set; }
    public IFormFile? ProfileCoverUpload { get; set; }
    public string? ProfileCoverUrl { get; set; }
}
