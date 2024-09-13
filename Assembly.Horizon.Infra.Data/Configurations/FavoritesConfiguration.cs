using Assembly.Horizon.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assembly.Horizon.Infra.Data.Configurations;

internal class FavoritesConfiguration : IEntityTypeConfiguration<Favorites>
{
    public void Configure(EntityTypeBuilder<Favorites> builder)
    {
        builder.ToTable("Favorites");
        builder.HasKey(x => x.Id);
    }
}
