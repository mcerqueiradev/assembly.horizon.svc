using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Domain.Core.Interfaces;
using Assembly.Horizon.Domain.Core.Uow;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Assembly.Horizon.Application.CQ.Comments.Queries.RetrieveByProperty;

public class RetrieveCommentsByPropertyQueryHandler(
    IUnitOfWork unitOfWork,
    ICommentRepository commentRepository,
    IUserRepository userRepository,
    IHttpContextAccessor httpContextAccessor,
    INotificationStrategy notificationStrategy) : IRequestHandler<RetrieveCommentsByPropertyQuery, Result<RetrieveCommentsByPropertyResponse, Success, Error>>
{

    public async Task<Result<RetrieveCommentsByPropertyResponse, Success, Error>> Handle(
        RetrieveCommentsByPropertyQuery request,
        CancellationToken cancellationToken)
    {
        var baseUrl = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";
        var comments = await unitOfWork.CommentRepository.GetCommentsByPropertyId(request.PropertyId);
        var parentComments = comments.Where(c => !c.ParentCommentId.HasValue);
        var commentDtos = new List<CommentDto>();

        foreach (var comment in parentComments)
        {
            var commentDto = await BuildCommentDtoWithReplies(comment, comments, baseUrl);
            commentDtos.Add(commentDto);
        }

        var response = new RetrieveCommentsByPropertyResponse
        {
            Comments = commentDtos
        };

        return response;
    }

    private async Task<CommentDto> BuildCommentDtoWithReplies(
        Comment comment,
        IEnumerable<Comment> allComments,
        string baseUrl)
    {
        var user = await userRepository.RetrieveAsync(comment.UserId);
        var replies = allComments.Where(c => c.ParentCommentId == comment.Id);
        var replyDtos = new List<CommentDto>();

        foreach (var reply in replies)
        {
            var replyDto = await BuildCommentDtoWithReplies(reply, allComments, baseUrl);
            replyDtos.Add(replyDto);
        }

        return new CommentDto
        {
            Id = comment.Id,
            PropertyId = comment.PropertyId,
            UserId = comment.UserId,
            Message = comment.Message,
            Rating = comment.Rating,
            DateTimePosted = comment.DateTimePosted,
            Flagged = comment.Flagged,
            CreatedAt = comment.CreatedAt,
            CreatedBy = comment.CreatedBy,
            ParentCommentId = comment.ParentCommentId,
            UserCommentName = $"{user.Name.FirstName} {user.Name.LastName}".Trim(),
            UserCommentEmail = user.Account.Email,
            UserCommentPhoto = user.ImageUrl != null ? $"{baseUrl}/uploads/{user.ImageUrl}" : null,
            HelpfulCount = comment.HelpfulCount,
            Replies = replyDtos
        };
    }
}
