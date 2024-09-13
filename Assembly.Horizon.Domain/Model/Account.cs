using Assembly.Horizon.Domain.Interface;
using System.Security.Principal;

namespace Assembly.Horizon.Domain.Model;

public class Account : IEntity<Guid>
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public bool IsActive { get; set; }
    public DateTime? LastActiveDate { get; set; }
    public Account()
    {
        Id = Guid.NewGuid();
    }

    public Account(string email, byte[] passwordHash, byte[] passwordSalt, bool isActive, DateTime? lastActiveDate) : this()
    {
        Email = email;
        PasswordHash = passwordHash;
        PasswordSalt = passwordSalt;
        IsActive = isActive;
        LastActiveDate = lastActiveDate;
    }
}