using Ecommerce.Core.Data;
using Ecommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Core.Repository;

public class ProductRepository : IProductRepository {
    private readonly BankContext _context;

    public ProductRepository(BankContext context) {
        _context = context;
    }

    public async Task<IEnumerable<ProductEntity>> FindAll() {
        return await _context.Set<ProductEntity>()
            .Include(p => p.category)
            .Include(p => p.variations)
                .ThenInclude(v => v.platform)
            .AsNoTracking()
            .ToListAsync();
    }
}