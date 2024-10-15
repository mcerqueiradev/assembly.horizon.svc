using Assembly.Horizon.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assembly.Horizon.Infra.Data.Configurations;

internal class RealtorConfiguration : IEntityTypeConfiguration<Realtor>
{
    public void Configure(EntityTypeBuilder<Realtor> builder)
    {
        builder.ToTable("Realtors");
        builder.HasKey(r => r.Id);

        // Configurando a relação um-para-um entre Realtor e User
        builder
            .HasOne(r => r.User) // Realtor tem um User
            .WithOne() // Você pode especificar a propriedade se houver (Ex: .WithOne(u => u.Realtor))
            .HasForeignKey<Realtor>(r => r.UserId) // Supondo que Realtor tenha um UserId
            .OnDelete(DeleteBehavior.Cascade); // Define o comportamento de deleção (opcional)

        // Configurando a relação um-para-muitos entre Realtor e Property
        builder
            .HasMany(r => r.Properties) // Realtor pode ter muitas Properties
            .WithOne(p => p.Realtor) // Cada Property tem um Realtor
            .HasForeignKey(p => p.RealtorId) // Supondo que Property tenha um RealtorId
            .OnDelete(DeleteBehavior.Cascade); // Define o comportamento de deleção (opcional)
    }
}
