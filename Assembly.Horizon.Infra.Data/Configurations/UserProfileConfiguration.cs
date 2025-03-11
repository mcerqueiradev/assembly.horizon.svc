using Assembly.Horizon.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assembly.Horizon.Infra.Data.Configurations;
public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
{
    public void Configure(EntityTypeBuilder<UserProfile> builder)
    {
        builder.ToTable("UserProfiles");

        builder.HasKey(x => x.UserId);

        builder.Property(x => x.Bio)
            .HasMaxLength(500);

        builder.Property(x => x.Location)
            .HasMaxLength(100);

        builder.Property(x => x.Website)
            .HasMaxLength(255);

        builder.Property(x => x.Occupation)
            .HasMaxLength(100);

        builder.HasOne(x => x.User)
            .WithOne(x => x.Profile)
            .HasForeignKey<UserProfile>(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
