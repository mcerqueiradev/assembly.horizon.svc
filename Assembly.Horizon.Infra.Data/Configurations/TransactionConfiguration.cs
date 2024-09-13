using Assembly.Horizon.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assembly.Horizon.Infra.Data.Configurations
{
    internal class TransactionsConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transactions");

            builder.HasKey(t => t.Id);

            builder.HasOne(t => t.User)
                .WithMany()
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.Property)
                .WithMany()
                .HasForeignKey(t => t.PropertyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.Realtor)
                .WithMany()
                .HasForeignKey(t => t.RealtorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(t => t.Value).IsRequired();
            builder.Property(t => t.Date).IsRequired();
            builder.Property(t => t.Description).IsRequired();
            builder.Property(t => t.Invoice).IsRequired();
            builder.Property(t => t.PaymentMethod).IsRequired();
            builder.Property(t => t.TransactionStatus).IsRequired();
            builder.Property(t => t.TransactionHistory).IsRequired();
        }
    }
}
