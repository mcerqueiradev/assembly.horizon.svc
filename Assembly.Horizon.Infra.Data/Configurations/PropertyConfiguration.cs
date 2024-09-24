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

        // Relacionamento 1:N entre Property e PropertyFile (Images)
        builder.HasMany(p => p.Images)
            .WithOne(img => img.Property)
            .HasForeignKey(img => img.PropertyId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configuração do campo Price
        builder.Property(p => p.Price)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        // Relacionamento 1:N entre Realtor e Property
        builder.HasOne(p => p.Realtor)
            .WithMany(r => r.Properties) // Um Realtor pode ter várias propriedades
            .HasForeignKey(p => p.RealtorId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relacionamento 1:N entre Customer (Owner) e Property
        builder.HasOne(p => p.Owner)             // Uma Property tem 1 Owner
            .WithMany(c => c.OwnedProperties)    // Um Customer (Owner) tem várias Properties
            .HasForeignKey(p => p.OwnerId)       // Chave estrangeira OwnerId
            .OnDelete(DeleteBehavior.Restrict);  // Restrição ao deletar
    }
}
