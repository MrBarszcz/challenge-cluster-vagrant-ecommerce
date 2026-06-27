namespace Ecommerce.Core.Entities;

public class ProductVariationEntity {
    public Guid Id { get; private set; }
    
    public decimal price { get; private set; }
    public int stockQuantity { get; private set; }

    public Guid productId { get; private set; }
    public Guid platformId { get; private set; }

    public ProductEntity product { get; private set; }
    public PlatformEntity platform { get; private set; }

    protected ProductVariationEntity() { }

    public ProductVariationEntity(decimal priceIn, int stockQuantityIn, Guid productIdIn, Guid platformIdIn) {
        Id = Guid.NewGuid();
        price = priceIn;
        stockQuantity = stockQuantityIn;
        productId = productIdIn;
        platformId = platformIdIn;
    }
}