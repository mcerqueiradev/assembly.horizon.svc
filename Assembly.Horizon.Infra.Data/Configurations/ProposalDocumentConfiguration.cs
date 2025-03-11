using Assembly.Horizon.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assembly.Horizon.Infra.Data.Configurations;

public class ProposalDocumentConfiguration : IEntityTypeConfiguration<ProposalDocument>
{
    public void Configure(EntityTypeBuilder<ProposalDocument> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.DocumentName)
               .IsRequired()
               .HasMaxLength(255);

        builder.Property(x => x.DocumentPath)
               .IsRequired()
               .HasMaxLength(1000);

        builder.Property(x => x.ContentType)
               .HasMaxLength(100);

        builder.Property(x => x.Description)
               .HasMaxLength(500);

        builder.HasOne(x => x.Proposal)
               .WithMany(x => x.Documents)
               .HasForeignKey(x => x.ProposalId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Negotiation)
               .WithMany(x => x.Documents)
               .HasForeignKey(x => x.NegotiationId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
