using Ecommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Core.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<CategoryEntity> {
    public void Configure(EntityTypeBuilder<CategoryEntity> builder) {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.category)
            .IsRequired()
            .HasMaxLength(100);
    }
}