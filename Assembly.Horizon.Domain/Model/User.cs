using Assembly.Horizon.Domain.Common;
using Assembly.Horizon.Domain.Interface;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assembly.Horizon.Domain.Model;

public class User : AuditableEntity, IEntity<Guid>
{
    public Guid Id { get; set; }
    public Name Name { get; set; }
    public Account Account { get; set; }
    public Access Access { get; set; }
    public string? ImageUrl { get; set; }
    [NotMapped]
    public IFormFile? Upload { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public List<Property> FavoriteProperties { get; set; } = new();
    public bool IsActive { get; set; }
    public DateTime? LastActiveDate { get; set; }

    public User()
    {
        Id = Guid.NewGuid();
    }
    public User(string firstName, string lastName, string email, byte[] passwordHash, byte[] passwordSalt, Access access, string imageUrl, string phoneNumber, DateTime dateOfBirth, bool isActive, DateTime? lastActiveDate)
    {
        Id = Guid.NewGuid();
        Name = new(firstName, lastName);
        Account = new(email, passwordHash, passwordSalt, isActive, lastActiveDate);
        Access = access;
        ImageUrl = imageUrl;
        PhoneNumber = phoneNumber;
        DateOfBirth = dateOfBirth;
        IsActive = isActive;
        LastActiveDate = lastActiveDate;
    }
}

public enum Access
{
    None,
    Guest,
    User,
    Realtor,
    Admin,
    Disabled
}

