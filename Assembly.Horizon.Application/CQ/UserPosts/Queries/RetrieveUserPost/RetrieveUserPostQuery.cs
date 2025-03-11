using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Domain.Core.Uow;
using Assembly.Horizon.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Assembly.Horizon.Application.CQ.UserPosts.Queries.RetrieveUserPost;

public class RetrieveUserPostQuery : IRequest<Result<RetrieveUserPostResponse, Success, Error>>
{
    public Guid Id { get; set; }
}

public class RetrieveUserPostQueryHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<RetrieveUserPostQuery, Result<RetrieveUserPostResponse, Success, Error>>
{
    public async Task<Result<RetrieveUserPostResponse, Success, Error>> Handle(RetrieveUserPostQuery request, CancellationToken cancellationToken)
    {
        var baseUrl = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";
        var post = await unitOfWork.UserPostRepository.RetrieveAsync(request.Id);

        if (post == null)
            return Error.NotFound;

        var response = new RetrieveUserPostResponse
        {
            Id = post.Id,
            UserId = post.UserId,
            Content = post.Content,
            Type = post.Type,
            IsActive = post.IsActive,
            CreatedAt = post.CreatedAt,
            LikesCount = post.LikesCount,
            SharesCount = post.SharesCount,
            MediaUrl = post.MediaUrl != null ? $"{baseUrl}/uploads/{post.MediaUrl}" : null,
        };

        return response;
    }
}

public class RetrieveUserPostResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Content { get; set; }
    public PostType Type { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public int LikesCount { get; set; }
    public int SharesCount { get; set; }
    public string? MediaUrl { get; set; }
}
