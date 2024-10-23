using Assembly.Horizon.Application.Common.Responses;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Categories.Queries.Retrieve;

public class RetrieveCategoryQuery : IRequest<Result<RetrieveCategoryResponse, Success, Error>>
{
    public Guid Id { get; init; }
}
