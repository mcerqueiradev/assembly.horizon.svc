using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Domain.Core.Interfaces;
using Assembly.Horizon.Domain.Core.Uow;
using Assembly.Horizon.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Assembly.Horizon.Application.CQ.UserPosts.Commands.Create;

public class CreateUserPostCommand : IRequest<Result<CreateUserPostResponse, Success, Error>>
{
    public Guid UserId { get; set; }
    public string Content { get; set; }
    public IFormFile? MediaFile { get; set; }
}

public class CreateUserPostCommandHandler(IUnitOfWork unitOfWork, IFileStorageService fileStorageService)
    : IRequestHandler<CreateUserPostCommand, Result<CreateUserPostResponse, Success, Error>>
{
    public async Task<Result<CreateUserPostResponse, Success, Error>> Handle(CreateUserPostCommand request, CancellationToken cancellationToken)
    {
        var postType = DeterminePostType(request.Content);

        var post = new UserPost{
            UserId = request.UserId,
            Content = request.Content,
            Type = postType,
            MediaFile = request.MediaFile,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        if (request.MediaFile != null)
        {
            var fileName = await fileStorageService.SaveFileAsync(request.MediaFile, cancellationToken);
            post.MediaUrl = fileName;
        }

        await unitOfWork.UserPostRepository.AddAsync(post);
        await unitOfWork.CommitAsync();

        var response = new CreateUserPostResponse
        {
            Id = post.Id,
            Content = post.Content,
            Type = post.Type,
            MediaUrl = post.MediaUrl,
            MediaType = post.MediaType,
            CreatedAt = post.CreatedAt
        };

        return response;
    }

    private PostType DeterminePostType(string content)
    {
        string[] systemUpdateKeywords = {
        "update",
        "maintenance",
        "system",
        "upgrade",
        "release",
        "version",
        "patch",
        "changelog",
        "feature",
        "improvement",
        "enhancement",
        "fix",
        "bugfix"
    };

        if (systemUpdateKeywords.Any(keyword =>
            content.Contains(keyword, StringComparison.OrdinalIgnoreCase)))
        {
            return PostType.SystemUpdate;
        }

        if (content.Contains("New Property", StringComparison.OrdinalIgnoreCase))
        {
            return PostType.NewProperty;
        }

        return PostType.UserIdea;
    }
}


public class CreateUserPostResponse
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public PostType Type { get; set; }
    public string? MediaUrl { get; set; }
    public MediaType? MediaType { get; set; }
    public DateTime CreatedAt { get; set; }
}
