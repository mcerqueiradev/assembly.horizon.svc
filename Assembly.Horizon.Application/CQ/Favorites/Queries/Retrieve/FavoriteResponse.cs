using Assembly.Horizon.Application.CQ.Properties.Queries.Retrieve;

namespace Assembly.Horizon.Application.CQ.Favorites.Queries.Retrieve;

public class FavoriteResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid PropertyId { get; set; }
    public Guid CategoryId { get; set; }
    public DateTime DateAdded { get; set; }
    public string Notes { get; set; }
    public RetrievePropertyResponse Property { get; set; }
}