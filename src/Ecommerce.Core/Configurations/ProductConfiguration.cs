using Ecommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Core.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<ProductEntity> {
    public void Configure(EntityTypeBuilder<ProductEntity> builder) {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(p => p.description)
            .HasMaxLength(500);

        // Relacionamento 1:N com a Categoria
        builder.HasOne(p => p.category)
            .WithMany(c => c.products)
            .HasForeignKey(p => p.categoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(p => p.variations)
            .WithOne(v => v.product)
            .HasForeignKey(v => v.productId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}