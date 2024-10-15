using Assembly.Horizon.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assembly.Horizon.Infra.Data.Configurations;

internal class ContractConfiguration : IEntityTypeConfiguration<Contract>
{
    public void Configure(EntityTypeBuilder<Contract> builder)
    {
        builder.ToTable("Contracts");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Value).HasColumnType("decimal(18,2)");
        builder.Property(c => c.AdditionalFees).HasColumnType("decimal(18,2)");
        builder.Property(c => c.SecurityDeposit).HasColumnType("decimal(18,2)");

        builder.Property(c => c.PaymentFrequency).HasMaxLength(50);
        builder.Property(c => c.InsuranceDetails).HasMaxLength(500);
        builder.Property(c => c.Notes).HasMaxLength(1000);
        builder.Property(c => c.DocumentPath).HasMaxLength(255);
        builder.Property(c => c.TemplateVersion).HasMaxLength(50);
        builder.HasOne(c => c.Property)
               .WithMany()
               .HasForeignKey(c => c.PropertyId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Customer)
               .WithMany()
               .HasForeignKey(c => c.CustomerId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Realtor)
               .WithMany()
               .HasForeignKey(c => c.RealtorId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
