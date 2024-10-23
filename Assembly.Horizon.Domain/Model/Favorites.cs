using Assembly.Horizon.Domain.Interface;

namespace Assembly.Horizon.Domain.Model;

public class Favorites : IEntity<Guid>
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Guid PropertyId { get; set; }
    public Property Property { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
    public DateTime DateAdded { get; set; }
    public string Notes { get; set; }

    public Favorites()
    {
        Id = Guid.NewGuid();
    }
    public Favorites(Guid userId, Guid propertyId, Guid categoryId, DateTime dateAdded, string notes)
    {
        UserId = userId;
        PropertyId = propertyId;
        CategoryId = categoryId;
        DateAdded = dateAdded;
        Notes = notes;
    }
}
