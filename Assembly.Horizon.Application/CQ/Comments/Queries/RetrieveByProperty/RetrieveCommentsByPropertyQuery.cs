using Assembly.Horizon.Application.Common.Responses;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Comments.Queries.RetrieveByProperty;

public class RetrieveCommentsByPropertyQuery : IRequest<Result<RetrieveCommentsByPropertyResponse, Success, Error>>
{
    public Guid PropertyId { get; init; }
}