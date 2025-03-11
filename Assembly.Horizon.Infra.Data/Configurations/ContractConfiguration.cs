using Assembly.Horizon.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assembly.Horizon.Infra.Data.Configurations;

internal class ContractConfiguration : IEntityTypeConfiguration<Contract>
{
    public void Configure(EntityTypeBuilder<Contract> builder)
    {
        builder.ToTable(nameof(Contract));

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Value).HasColumnType("decimal(18,2)");
        builder.Property(c => c.AdditionalFees).HasColumnType("decimal(18,2)");
        builder.Property(c => c.SecurityDeposit).HasColumnType("decimal(18,2)");

        builder.Property(c => c.PaymentFrequency).HasMaxLength(50);
        builder.Property(c => c.InsuranceDetails).HasMaxLength(2000);
        builder.Property(c => c.Notes).HasMaxLength(1000);
        builder.Property(c => c.DocumentPath).HasMaxLength(255);

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

        builder.HasOne(c => c.Proposal)
               .WithMany()
               .HasForeignKey(c => c.ProposalId)
               .IsRequired(false)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(c => c.Invoices)
               .WithOne(i => i.Contract)
               .HasForeignKey(i => i.ContractId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.Transactions)
               .WithOne(t => t.Contract)
               .HasForeignKey(t => t.ContractId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
