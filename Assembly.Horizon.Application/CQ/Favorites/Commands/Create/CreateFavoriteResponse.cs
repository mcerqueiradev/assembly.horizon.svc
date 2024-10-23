namespace Assembly.Horizon.Application.CQ.Favorites.Commands.Create;

public class CreateFavoriteResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid PropertyId { get; set; }
    public Guid CategoryId { get; set; }
    public DateTime DateAdded { get; set; }
    public string Notes { get; set; }
}
