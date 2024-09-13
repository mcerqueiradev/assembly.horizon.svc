using Assembly.Horizon.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assembly.Horizon.Infra.Data.Configurations;

internal class PropertyConfiguration : IEntityTypeConfiguration<Property>
{
    public void Configure(EntityTypeBuilder<Property> builder)
    {
        builder.ToTable("Properties");
        builder.HasKey(p => p.Id);

        builder.HasOne(p => p.Address)
        .WithOne()
        .HasForeignKey<Property>(p => p.AddressId)
        .IsRequired()
        .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(c => c.Images)
        .WithOne(c => c.Property)
        .HasForeignKey(c => c.PropertyId)
        .OnDelete(DeleteBehavior.Restrict);

        builder.Property(p => p.Price)
            .HasColumnType("decimal(18,2)")
        .IsRequired();

        builder
        .HasOne(p => p.Realtor)
        .WithMany(r => r.Properties)
        .HasForeignKey(p => p.RealtorId)
        .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.User)
               .WithMany()
               .HasForeignKey(p => p.UserId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
