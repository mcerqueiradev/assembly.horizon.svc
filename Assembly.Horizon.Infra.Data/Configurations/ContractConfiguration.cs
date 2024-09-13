using Assembly.Horizon.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assembly.Horizon.Infra.Data.Configurations
{
    internal class ContractConfiguration : IEntityTypeConfiguration<Contract>
    {
        public void Configure(EntityTypeBuilder<Contract> builder)
        {
            builder.ToTable("Contracts");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.StartDate)
                   .IsRequired();

            builder.Property(c => c.EndDate)
                   .IsRequired();

            builder.Property(c => c.Value)
                   .IsRequired();

            builder.Property(c => c.TermsAndConditions);

            builder.Property(c => c.AdditionalFees)
                   .IsRequired();

            builder.Property(c => c.PaymentFrequency);

            builder.Property(c => c.RenewalOption)
                   .IsRequired();

            builder.Property(c => c.TerminationClauses);

            builder.Property(c => c.IsActive)
                   .IsRequired();

            builder.Property(c => c.LastActiveDate);

            builder.HasOne(c => c.Property)
                   .WithMany()
                   .HasForeignKey(c => c.PropertyId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.User)
                   .WithMany() // Presumindo que um User pode ter muitos Contracts, ajuste se necessário
                   .HasForeignKey(c => c.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Realtor)
                   .WithMany() // Presumindo que um Realtor pode ter muitos Contracts, ajuste se necessário
                   .HasForeignKey(c => c.RealtorId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
