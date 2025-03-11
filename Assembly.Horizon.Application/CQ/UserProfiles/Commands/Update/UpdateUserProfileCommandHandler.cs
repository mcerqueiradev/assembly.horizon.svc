using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Domain.Core.Interfaces;
using Assembly.Horizon.Domain.Core.Uow;
using MediatR;

namespace Assembly.Horizon.Application.CQ.UserProfiles.Commands.Update;

public class UpdateUserProfileCommandHandler(IUnitOfWork unitOfWork, IFileStorageService fileStorageService) : IRequestHandler<UpdateUserProfileCommand, Result<UpdateUserProfileResponse, Success, Error>>
{

    public async Task<Result<UpdateUserProfileResponse, Success, Error>> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
    {
        var existingProfile = await unitOfWork.UserProfileRepository.RetrieveAsync(request.Id);

        if (existingProfile == null)
        {
            return Error.NotFound;
        }

        existingProfile.Bio = request.Bio;
        existingProfile.Location = request.Location;
        existingProfile.Website = request.Website;
        existingProfile.Occupation = request.Occupation;

        if (request.ProfileCoverUpload != null)
        {
            var fileName = await fileStorageService.SaveFileAsync(request.ProfileCoverUpload, cancellationToken);
            existingProfile.ProfileCoverUrl = fileName;
        }
        else if (!string.IsNullOrEmpty(request.ProfileCoverUrl))
        {
            var coverUrlFileName = Path.GetFileName(request.ProfileCoverUrl);
            existingProfile.ProfileCoverUrl = coverUrlFileName;
        }

        unitOfWork.UserProfileRepository.UpdateAsync(existingProfile);
        await unitOfWork.CommitAsync();

        var response = new UpdateUserProfileResponse
        {
            Id = existingProfile.Id,
            Bio = existingProfile.Bio,
            ProfileCoverUrl = existingProfile.ProfileCoverUrl,
            Location = existingProfile.Location,
            Website = existingProfile.Website,
            Occupation = existingProfile.Occupation
        };

        return response;
    }
}
