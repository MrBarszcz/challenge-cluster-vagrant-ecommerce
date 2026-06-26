namespace Ecommerce.Core.Entities;

public class PlatformEntity {
    public Guid Id { get; private set; }
    public string? platform { get; private set; }

    public ICollection<ProductVariationEntity> variations { get; private set; }

    protected PlatformEntity() { }

    public PlatformEntity(string platformIn) {
        Id = Guid.NewGuid();
        platform = platformIn;
        variations = new List<ProductVariationEntity>();
    }

}