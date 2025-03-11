namespace Assembly.Horizon.Application.CQ.Favorites.Commands.Remove;

public record RemoveFavoriteResponse
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public Guid PropertyId { get; init; }
    public Guid CategoryId { get; init; }
    public DateTime DateRemoved { get; init; }
}
