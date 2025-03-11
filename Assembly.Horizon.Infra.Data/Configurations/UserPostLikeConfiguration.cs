using Assembly.Horizon.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assembly.Horizon.Infra.Data.Configurations;

public class UserPostLikeConfiguration : IEntityTypeConfiguration<UserPostLike>
{
    public void Configure(EntityTypeBuilder<UserPostLike> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Post)
            .WithMany(x => x.Likes)
            .HasForeignKey(x => x.PostId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new { x.PostId, x.UserId })
            .IsUnique();
    }
}
