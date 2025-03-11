using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Application.CQ.UserPosts.Queries.RetrieveUserPost;
using Assembly.Horizon.Domain.Core.Uow;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Assembly.Horizon.Application.CQ.UserPosts.Queries.RetrieveAllUserPost;

public class RetrieveAllUserPostQuery : IRequest<Result<RetrieveAllUserPostResponse, Success, Error>>
{
    public Guid UserId { get; set; }
}

public class RetrieveAllUserPostQueryHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<RetrieveAllUserPostQuery, Result<RetrieveAllUserPostResponse, Success, Error>>
{
    public async Task<Result<RetrieveAllUserPostResponse, Success, Error>> Handle(RetrieveAllUserPostQuery request, CancellationToken cancellationToken)
    {
        var baseUrl = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";

        var posts = await unitOfWork.UserPostRepository.GetPostsByUserIdAsync(request.UserId);

        var responses = posts.Select(post => new RetrieveUserPostResponse
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
        });


        return new RetrieveAllUserPostResponse
        {
            Responses = responses
        };
    }
}

public class RetrieveAllUserPostResponse
{
    public IEnumerable<RetrieveUserPostResponse> Responses { get; set; }
}


