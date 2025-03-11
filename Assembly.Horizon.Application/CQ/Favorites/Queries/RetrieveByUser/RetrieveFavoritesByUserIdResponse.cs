using Assembly.Horizon.Application.CQ.Favorites.Queries.Retrieve;

namespace Assembly.Horizon.Application.CQ.Favorites.Queries.RetrieveByUser;

public class RetrieveFavoritesByUserIdResponse
{
    public IEnumerable<FavoriteResponse> Favorites { get; set; }
}
