using Assembly.Horizon.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assembly.Horizon.Infra.Data.Configurations;

public class UserPostConfiguration : IEntityTypeConfiguration<UserPost>
{
    public void Configure(EntityTypeBuilder<UserPost> builder)
    {
        builder.ToTable("UserPosts");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Content)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(x => x.Type)
            .IsRequired();

        builder.Property(x => x.MediaUrl)
            .HasMaxLength(500)
            .IsUnicode(false);

        builder.Property(x => x.MediaType)
            .HasConversion<string>();

        builder.Ignore(x => x.MediaFile);

        builder.HasOne(x => x.User)
            .WithMany(x => x.Posts)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
