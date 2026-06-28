using Ecommerce.Core.Entities;

namespace Ecommerce.Core.Repository;

public interface IProductRepository {
    Task<IEnumerable<ProductEntity>> FindAll();
    Task<ProductEntity> FindById(Guid id);
}