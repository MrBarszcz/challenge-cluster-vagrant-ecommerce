using Ecommerce.Core.Entities;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Core.Data;

public class BankContext : DbContext {
    public BankContext(DbContextOptions<BankContext> options) : base(options) {
    }

    public DbSet<CategoryEntity> Categories { get; set; }
    public DbSet<PlatformEntity> Platforms { get; set; }
    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<ProductVariationEntity> ProductVariations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        base.OnModelCreating(modelBuilder);
    }
}