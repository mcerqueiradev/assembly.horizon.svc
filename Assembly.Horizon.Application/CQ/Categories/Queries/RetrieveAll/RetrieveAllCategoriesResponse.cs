using Assembly.Horizon.Application.CQ.Categories.Queries.Retrieve;

namespace Assembly.Horizon.Application.CQ.Categories.Queries.RetrieveAll;

public class RetrieveAllCategoriesResponse
{
    public IEnumerable<RetrieveCategoryResponse> Categories { get; set; } = new List<RetrieveCategoryResponse>();
}
