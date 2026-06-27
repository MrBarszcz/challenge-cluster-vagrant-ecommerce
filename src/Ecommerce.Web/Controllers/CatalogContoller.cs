using Ecommerce.Core.Repository;
using Ecommerce.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Controllers;

public class CatalogController : Controller {
    private readonly IProductRepository _productRepository;
    
    public CatalogController(IProductRepository productRepository) {
        _productRepository = productRepository;
    }

    public async Task<IActionResult> Index() {
        var products = await _productRepository.FindAll();

        var catalogCards = products.Select(p => new ProductCardViewModel {
            ProductId = p.Id,
            Name = p.name,
            ImageUrl = p.imageUrl ?? string.Empty,
            
            CategoryName = p.category != null ? p.category.category : "Sem categoria",
            
            LowestPrice = p.variations.Any() ? p.variations.Min(v => v.price) : 0,
            
            Platforms = p.variations
                .Where(v => v.platform != null)
                .Select(v => v.platform.platform)
                .Distinct()
                .ToList()!
        }).ToList();

        return View(catalogCards);
    }
}