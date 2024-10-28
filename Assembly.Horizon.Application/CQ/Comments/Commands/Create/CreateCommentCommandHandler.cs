using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Domain.Core.Interfaces;
using Assembly.Horizon.Domain.Core.Uow;
using Assembly.Horizon.Domain.Model;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Comments.Commands.Create;

public class CreateCommentCommandHandler(
    IUnitOfWork unitOfWork,
    ICommentRepository commentRepository,
    INotificationStrategy notificationStrategy)
    : IRequestHandler<CreateCommentCommand, Result<CreateCommentResponse, Success, Error>>
{
    public async Task<Result<CreateCommentResponse, Success, Error>> Handle(
      CreateCommentCommand request,
      CancellationToken cancellationToken)
    {
        var comment = new Comment(
             Guid.NewGuid(),
             request.PropertyId,
             request.UserId,
             request.Message,
             request.Rating,
             DateTime.UtcNow,
             false,
             request.ParentCommentId)
        {
            CreatedAt = DateTime.UtcNow,
            CreatedBy = request.UserId.ToString()
        };

        await commentRepository.AddAsync(comment);
        await unitOfWork.CommitAsync(cancellationToken);

        var property = await unitOfWork.PropertyRepository.RetrieveAsync(request.PropertyId, cancellationToken);
        var realtor = await unitOfWork.RealtorRepository.RetrieveAsync(property.RealtorId, cancellationToken);
        var user = await unitOfWork.UserRepository.RetrieveAsync(request.UserId, cancellationToken);


        if (request.ParentCommentId.HasValue)
        {
            var parentComment = await commentRepository.RetrieveAsync(request.ParentCommentId.Value, cancellationToken);

            // Notify parent comment user
            var parentCommentNotification = new Notification(
                user.Id,
                parentComment.UserId,
                $"{user.Name.FirstName} {user.Name.LastName} replied to your comment on {property.Title}",
                NotificationType.Message,
                NotificationPriority.Low,
                property.Id,
                "Property"
            );
            await notificationStrategy.StoreTransientNotification(parentCommentNotification);

            // Notify realtor
            var realtorNotification = new Notification(
                user.Id,
                realtor.UserId,
                $"{user.Name.FirstName} {user.Name.LastName} replied to a comment on {property.Title}",
                NotificationType.Message,
                NotificationPriority.Low,
                property.Id,
                "Property"
            );
            await notificationStrategy.StoreTransientNotification(realtorNotification);
        }
        else
        {
            // For new comments, notify only the realtor
            var notification = new Notification(
                user.Id,
                realtor.UserId,
                $"{user.Name.FirstName} {user.Name.LastName} added a comment on {property.Title}",
                NotificationType.Message,
                NotificationPriority.Low,
                property.Id,
                "Property"
            );
            await notificationStrategy.StoreTransientNotification(notification);
        }

        var response = new CreateCommentResponse
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
        };

        return response;
    }
}
