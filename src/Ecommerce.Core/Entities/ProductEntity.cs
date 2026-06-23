namespace Ecommerce.Core.Entities;

public class ProductEntity {
    public Guid Id { get; private set; }
    public string name { get; private set; }
    public string description { get; private set; }

    // Chaves Estrangeiras (Foreign Keys)
    public Guid categoryId { get; private set; }

    // Propriedades de Navegação
    public CategoryEntity category { get; private set; }

    public ProductEntity(string nameIn, string descriptionIn, Guid categoryIdIn) {
        Id = Guid.NewGuid();
        name = nameIn;
        description = descriptionIn;
        categoryId = categoryIdIn;
    }
}