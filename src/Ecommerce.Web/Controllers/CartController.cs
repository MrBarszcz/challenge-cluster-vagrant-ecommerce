using System.Text.Json;
using Ecommerce.Core.Repository;
using Ecommerce.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Controllers;

public class CartController : Controller {
    private readonly IProductRepository _repository;

    public CartController(IProductRepository repository) {
        _repository = repository;
    }

    [HttpPost]
    public IActionResult Add(Guid variationId) {
        var cartJson = HttpContext.Session.GetString("Cart") ?? "[]";
        var cartIds = JsonSerializer.Deserialize<List<Guid>>(cartJson);

        cartIds!.Add(variationId);
        HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(cartIds));

        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Index() {
        var cartJson = HttpContext.Session.GetString("Cart") ?? "[]";
        var cartIds = JsonSerializer.Deserialize<List<Guid>>(cartJson);

        var cartItems = new List<CartItemViewModel>();

        if (cartIds != null) {
            foreach (var id in cartIds) {
                var variation = await _repository.FindByVariationId(id);
                if (variation != null) {
                    cartItems.Add(new CartItemViewModel {
                        VariationId = variation.Id,
                        ProductName = variation.product?.name ?? "Jogo",
                        PlatformName = variation.platform?.platform ?? "Plataforma",
                        Price = variation.price,
                        ImageUrl = variation.product?.imageUrl ?? string.Empty
                    });
                }
            }
        }

        return View(cartItems);
    }
}