using Assembly.Horizon.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assembly.Horizon.Infra.Data.Configurations;

internal class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable(nameof(Transaction));
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Amount).HasColumnType("decimal(18,2)");
        builder.Property(t => t.Description).HasMaxLength(500);
        builder.Property(t => t.PaymentMethod).HasMaxLength(50);
        builder.Property(t => t.TransactionHistory).HasMaxLength(1000);

        builder.HasOne(t => t.Contract)
               .WithMany(c => c.Transactions)
               .HasForeignKey(t => t.ContractId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.Invoice)
               .WithOne(i => i.Transaction)
               .HasForeignKey<Transaction>(t => t.InvoiceId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}