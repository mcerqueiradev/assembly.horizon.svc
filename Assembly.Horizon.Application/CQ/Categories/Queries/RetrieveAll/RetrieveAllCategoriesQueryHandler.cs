using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Application.CQ.Categories.Queries.Retrieve;
using Assembly.Horizon.Domain.Core.Uow;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Categories.Queries.RetrieveAll
{
    public class RetrieveAllCategoriesQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<RetrieveAllCategoriesQuery, Result<RetrieveAllCategoriesResponse, Success, Error>>
    {
        public async Task<Result<RetrieveAllCategoriesResponse, Success, Error>> Handle(RetrieveAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await unitOfWork.CategoryRepository.RetrieveAllAsync(cancellationToken);

            if (categories == null || !categories.Any())
            {
                return Error.NotFound;
            }

            var categoriesResponse = categories.Select(category => new RetrieveCategoryResponse
            {
                Id = category.Id,
                Name = category.Name,
            });

            var response = new RetrieveAllCategoriesResponse
            {
                Categories = categoriesResponse
            };

            return response;
        }
    }
}
