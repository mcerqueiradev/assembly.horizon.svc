using Assembly.Horizon.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assembly.Horizon.Infra.Data.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.OwnsOne(u => u.Name, n =>
        {
            n.Property(x => x.FirstName)
                .HasColumnName("FirstName")
                .IsRequired();

            n.Property(x => x.LastName)
                .HasColumnName("LastName")
                .IsRequired();
        });

        builder
        .HasMany(u => u.FavoriteProperties)
        .WithMany(p => p.LikedByUsers)
        .UsingEntity<Dictionary<string, object>>(
            "UserFavoriteProperties",
            j => j.HasOne<Property>()
                  .WithMany()
                  .HasForeignKey("PropertyId"),
            j => j.HasOne<User>()
                  .WithMany()
                  .HasForeignKey("UserId"),
            j =>
            {
                j.HasKey("UserId", "PropertyId");
            });

        builder.HasOne(x => x.Profile)
    .WithOne(x => x.User)
    .HasForeignKey<UserProfile>(x => x.UserId);

        builder.HasMany(x => x.Posts)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId);
    }
}

