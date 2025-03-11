using Assembly.Horizon.Application.CQ.Favorites.Queries.Retrieve;

namespace Assembly.Horizon.Application.CQ.Favorites.Queries.RetrieveByProperty;

public class RetrieveFavoritesByPropertyIdResponse
{
    public IEnumerable<FavoriteResponse> Favorites { get; set; }
}