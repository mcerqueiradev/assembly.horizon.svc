using Assembly.Horizon.Domain.Common;
using Assembly.Horizon.Domain.Interface;

namespace Assembly.Horizon.Domain.Model;

public class UserProfile : AuditableEntity, IEntity<Guid>
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string? Bio { get; set; }
    public string? ProfileCoverUrl { get; set; }
    public string? Location { get; set; }
    public string? Website { get; set; }
    public string? Occupation { get; set; }
    public virtual User User { get; set; }

    public UserProfile()
    {
    }

    public UserProfile(Guid userId, string bio, string profileCoverUrl, string location, string website, string occupation)
    {
        UserId = userId;
        Bio = bio;
        ProfileCoverUrl = profileCoverUrl;
        Location = location;
        Website = website;
        Occupation = occupation;
    }
}
