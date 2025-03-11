using Assembly.Horizon.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assembly.Horizon.Infra.Data.Configurations;

public class ProposalNegotiationConfiguration : IEntityTypeConfiguration<ProposalNegotiation>
{
    public void Configure(EntityTypeBuilder<ProposalNegotiation> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Message)
               .IsRequired()
               .HasMaxLength(1000);

        builder.Property(x => x.CounterOffer)
               .HasPrecision(18, 2);

        builder.Property(x => x.Status)
               .IsRequired();

        builder.HasOne(x => x.Proposal)
               .WithMany(x => x.Negotiations)
               .HasForeignKey(x => x.ProposalId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Sender)
               .WithMany()
               .HasForeignKey(x => x.SenderId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
