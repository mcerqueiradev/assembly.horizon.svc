using Assembly.Horizon.Domain.Common;
using Assembly.Horizon.Domain.Interface;

namespace Assembly.Horizon.Domain.Model;

public class UserPostShare : AuditableEntity, IEntity<Guid>
{
    public Guid Id { get; set; }
    public Guid PostId { get; set; }
    public Guid UserId { get; set; }
    public virtual UserPost Post { get; set; }
    public virtual User User { get; set; }
}
