using Assembly.Horizon.Domain.Interface;

namespace Assembly.Horizon.Domain.Model;

public class Category : IEntity<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Category(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}
