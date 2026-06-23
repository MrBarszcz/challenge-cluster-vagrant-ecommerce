using Ecommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Core.Configurations;

public class ProductVariationConfiguration : IEntityTypeConfiguration<ProductVariationEntity> {
    public void Configure(EntityTypeBuilder<ProductVariationEntity> builder) {
        builder.HasKey(pv => pv.Id);

        builder.Property(pv => pv.price)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(pv => pv.stockQuantity)
            .IsRequired();

        builder.HasOne(pv => pv.product)
            .WithMany(p => p.variations)
            .HasForeignKey(pv => pv.productId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(pv => pv.platform)
            .WithMany(p => p.variations)
            .HasForeignKey(pv => pv.platformId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}