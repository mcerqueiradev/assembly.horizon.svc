using Assembly.Horizon.Application.Common.Responses;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Categories.Queries.RetrieveAll
{
    public class RetrieveAllCategoriesQuery : IRequest<Result<RetrieveAllCategoriesResponse, Success, Error>>
    {
    }
}
