using Assembly.Horizon.Domain.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Assembly.Horizon.Infra.Data.Configurations;

internal class PropertyVisitConfiguration : IEntityTypeConfiguration<PropertyVisit>
{
    public void Configure(EntityTypeBuilder<PropertyVisit> builder)
    {
        builder.ToTable("PropertyVisits");
        builder.HasKey(r => r.Id);

        builder.HasOne(pv => pv.Property)
               .WithMany()
               .HasForeignKey(pv => pv.PropertyId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(pv => pv.Customer)
               .WithMany()
               .HasForeignKey(pv => pv.CustomerId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(pv => pv.Realtor)
               .WithMany()
               .HasForeignKey(pv => pv.RealtorId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
