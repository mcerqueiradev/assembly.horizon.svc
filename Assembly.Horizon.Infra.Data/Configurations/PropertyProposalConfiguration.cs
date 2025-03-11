using Assembly.Horizon.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assembly.Horizon.Infra.Data.Configurations;

public class PropertyProposalConfiguration : IEntityTypeConfiguration<PropertyProposal>
{
    public void Configure(EntityTypeBuilder<PropertyProposal> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ProposedValue)
               .HasPrecision(18, 2)
               .IsRequired();

        builder.Property(x => x.Type)
               .IsRequired();

        builder.Property(x => x.Status)
               .IsRequired();

        builder.Property(x => x.PaymentMethod)
               .IsRequired();

        builder.HasOne(x => x.Property)
               .WithMany()
               .HasForeignKey(x => x.PropertyId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.User)
               .WithMany()
               .HasForeignKey(x => x.UserId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}