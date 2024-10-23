using Assembly.Horizon.Domain.Interface;

namespace Assembly.Horizon.Domain.Model;

public class Category : IEntity<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public virtual ICollection<Property> Properties { get; set; }
    public virtual ICollection<Favorites> Favorites { get; set; }

    public Category(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public Category()
    {
        Properties = new HashSet<Property>();
        Favorites = new HashSet<Favorites>();
    }
}
