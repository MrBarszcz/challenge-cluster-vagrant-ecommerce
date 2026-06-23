namespace Ecommerce.Core.Entities;
public class CategoryEntity {
    public Guid Id { get; private set; }
    public string? category { get; private set; }   

    public ICollection<ProductEntity> products { get; private set; }
    
    public CategoryEntity(string categoryIn) {
        Id = Guid.NewGuid();
        category = categoryIn;
        products = new List<ProductEntity>();
    }
}

