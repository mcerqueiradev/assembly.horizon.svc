using Assembly.Horizon.Domain.Common;
using Assembly.Horizon.Domain.Interface;

namespace Assembly.Horizon.Domain.Model;

public class Realtor : AuditableEntity, IEntity<Guid>
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public List<Property> Properties { get; set; } = new();
    public string OfficeEmail { get; set; }
    public int TotalSales { get; set; }
    public int TotalListings { get; set; }
    public List<string> Certifications { get; set; }
    public List<Languages> LanguagesSpoken { get; set; } = new();

    public Realtor()
    {
    }

    public Realtor(User user, string officeEmail, int totalSales, int totalListings, List<string> certifications, List<Languages> languagesSpoken)
    {
        User = user;
        UserId = user.Id;
        OfficeEmail = officeEmail;
        TotalSales = totalSales;
        TotalListings = totalListings;
        Certifications = certifications;
        LanguagesSpoken = languagesSpoken ?? new List<Languages>();
        Properties = new List<Property>();
    }
}

public enum Languages
{
    English,
    Spanish,
    French,
    German,
    Italian,
    Portuguese
}