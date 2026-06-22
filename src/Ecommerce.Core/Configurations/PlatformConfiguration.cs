using Ecommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Core.Configurations;

public class PlatformConfiguration : IEntityTypeConfiguration<PlatformEntity> {
    public void Configure(EntityTypeBuilder<PlatformEntity> builder) {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.platform)
            .IsRequired()
            .HasMaxLength(100);
    }
}