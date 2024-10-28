using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Domain.Core.Interfaces;
using Assembly.Horizon.Domain.Core.Uow;
using Assembly.Horizon.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.Horizon.Application.CQ.Comments.Commands;

public class ToggleCommentHelpfulCommand : IRequest<Result<ToggleCommentHelpfulResponse, Success, Error>>
{
    public Guid CommentId { get; set; }
    public Guid UserId { get; set; }
    public bool IsHelpful { get; set; }
}

public class ToggleCommentHelpfulResponse
{
    public int HelpfulCount { get; set; }
    public bool IsHelpful { get; set; }
}

public class ToggleCommentHelpfulCommandHandler(IUnitOfWork unitOfWork, ICommentRepository commentRepository, INotificationStrategy notificationStrategy) : IRequestHandler<ToggleCommentHelpfulCommand, Result<ToggleCommentHelpfulResponse, Success, Error>>
{

    public async Task<Result<ToggleCommentHelpfulResponse, Success, Error>> Handle(
        ToggleCommentHelpfulCommand request,
        CancellationToken cancellationToken)
    {
        var comment = await commentRepository.RetrieveAsync(request.CommentId, cancellationToken);
        var existingLike = await commentRepository.GetCommentLikeAsync(request.CommentId, request.UserId, cancellationToken);

        if (existingLike != null)
        {
            comment.RemoveLike(existingLike);
            await commentRepository.RemoveCommentLikeAsync(existingLike, cancellationToken);
        }
        else
        {
            var newLike = new CommentLike(request.CommentId, request.UserId);
            comment.AddLike(newLike);
            await commentRepository.AddCommentLikeAsync(newLike, cancellationToken);

            var property = await unitOfWork.PropertyRepository.RetrieveAsync(comment.PropertyId, cancellationToken);
            var realtor = await unitOfWork.RealtorRepository.RetrieveAsync(property.RealtorId, cancellationToken);
            var user = await unitOfWork.UserRepository.RetrieveAsync(request.UserId, cancellationToken);

            var notification = new Notification(
                user.Id,
                realtor.UserId,
                $"{user.Name.FirstName} {user.Name.LastName} liked a comment on {property.Title}",
                NotificationType.Message,
                NotificationPriority.Low,
                property.Id,
                "Property"
            );

            await notificationStrategy.StoreTransientNotification(notification);
        }

        await unitOfWork.CommitAsync(cancellationToken);

        return new ToggleCommentHelpfulResponse
        {
            HelpfulCount = comment.HelpfulCount,
            IsHelpful = existingLike == null
        };
    }
}