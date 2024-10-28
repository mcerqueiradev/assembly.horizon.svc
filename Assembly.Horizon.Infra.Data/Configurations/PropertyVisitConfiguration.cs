using Assembly.Horizon.Domain.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Assembly.Horizon.Infra.Data.Configurations;

internal class PropertyVisitConfiguration : IEntityTypeConfiguration<PropertyVisit>
{
    public void Configure(EntityTypeBuilder<PropertyVisit> builder)
    {
        builder.ToTable("PropertyVisits", "Horizon");
        builder.HasKey(r => r.Id);

        builder.HasOne(pv => pv.Property)
               .WithMany()
               .HasForeignKey(pv => pv.PropertyId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(pv => pv.User)
               .WithMany()
               .HasForeignKey(pv => pv.UserId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(pv => pv.RealtorUser)
               .WithMany()
               .HasForeignKey(pv => pv.RealtorUserId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.Property(pv => pv.Notes)
               .HasMaxLength(500);
    }
}