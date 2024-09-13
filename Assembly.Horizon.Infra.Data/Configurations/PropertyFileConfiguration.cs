using Assembly.Horizon.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assembly.Horizon.Infra.Data.Configurations;

internal class PropertyFileConfiguration : IEntityTypeConfiguration<PropertyFile>
{
    public void Configure(EntityTypeBuilder<PropertyFile> builder)
    {
        builder.ToTable("PropertyFiles");
        builder.HasKey(x => x.Id);
    }
}
