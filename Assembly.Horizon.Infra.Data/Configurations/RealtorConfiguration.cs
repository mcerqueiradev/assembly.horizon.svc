using Assembly.Horizon.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Assembly.Horizon.Infra.Data.Configurations;

internal class RealtorConfiguration : IEntityTypeConfiguration<Realtor>
{
    public void Configure(EntityTypeBuilder<Realtor> builder)
    {
        builder.ToTable("Realtors");
        builder.HasKey(r => r.Id);

        builder
        .HasMany(r => r.Properties)
        .WithOne(p => p.Realtor)
        .HasForeignKey(p => p.RealtorId);
    }
}
