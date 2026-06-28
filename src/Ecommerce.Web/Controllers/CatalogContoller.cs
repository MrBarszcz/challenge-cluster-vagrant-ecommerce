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

    [HttpGet]
    public async Task<IActionResult> Details(Guid id) {
        var product = await _productRepository.FindById(id);

        if (product == null) return NotFound();

        var viewModel = new ProductDetailsViewModel {
            ProductId = product.Id,
            Name = product.name,
            Description = product.description,
            ImageUrl = product.imageUrl ?? string.Empty,
            CategoryName = product.category?.category ?? "Sem Categoria",
            
            Variations = product.variations.Select(v => new VariationOptionViewModel {
                VariationId = v.Id,
                PlatformName = v.platform?.platform ?? "Desconhecida",
                Price = v.price,
                Stock = v.stockQuantity
            }).ToList()
        };

        return View(viewModel);
    }
}