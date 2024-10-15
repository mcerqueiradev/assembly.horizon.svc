using Assembly.Horizon.Domain.Common;
using Assembly.Horizon.Domain.Interface;

namespace Assembly.Horizon.Domain.Model;

public class Customer : AuditableEntity, IEntity<Guid>
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Customer() { }

    public Customer(Guid id, User user)
    {
        Id = id;
        UserId = user.Id;
        User = user;
    }

    // Método para obter o tipo de acesso (CustomerType)
    public Access GetCustomerType()
    {
        return User.Access;
    }
}