using Assembly.Horizon.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assembly.Horizon.Infra.Data.Configurations;

internal class PropertyConfiguration : IEntityTypeConfiguration<Property>
{
    public void Configure(EntityTypeBuilder<Property> builder)
    {
        builder.ToTable("Properties");

        // Chave primária
        builder.HasKey(p => p.Id);

        // Relacionamento 1:1 entre Property e Address
        builder.HasOne(p => p.Address)
            .WithOne()
            .HasForeignKey<Property>(p => p.AddressId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(p => p.Price)
           .HasPrecision(18, 2);  // 18 dígitos no total, 2 casas decimais

        // Relacionamento 1:N entre Property e PropertyFile (Images)
        builder.HasMany(p => p.Images)
            .WithOne(img => img.Property)
            .HasForeignKey(img => img.PropertyId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relacionamento 1:N entre Realtor e Property
        builder.HasOne(p => p.Realtor)
            .WithMany(r => r.Properties) // Um Realtor pode ter várias propriedades
            .HasForeignKey(p => p.RealtorId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
