using Assembly.Horizon.Application.Common.Responses;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Comments.Commands.Create;

public class CreateCommentCommand : IRequest<Result<CreateCommentResponse, Success, Error>>
{
    public Guid PropertyId { get; set; }
    public Guid UserId { get; set; }
    public string Message { get; set; }
    public int? Rating { get; set; }
    public Guid? ParentCommentId { get; set; }
}