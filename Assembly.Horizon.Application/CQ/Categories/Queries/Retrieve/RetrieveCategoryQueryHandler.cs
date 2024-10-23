using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Domain.Core.Uow;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Categories.Queries.Retrieve;

public class RetrieveCategoryQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<RetrieveCategoryQuery, Result<RetrieveCategoryResponse, Success, Error>>
{
    public async Task<Result<RetrieveCategoryResponse, Success, Error>> Handle(RetrieveCategoryQuery request, CancellationToken cancellationToken)
    {
        var category = await unitOfWork.CategoryRepository.RetrieveAsync(request.Id);

        if (category == null)
        {
            return Error.NotFound;
        }

        var response = new RetrieveCategoryResponse
        {
            Id = category.Id,
            Name = category.Name
        };

        return response;
    }
}
