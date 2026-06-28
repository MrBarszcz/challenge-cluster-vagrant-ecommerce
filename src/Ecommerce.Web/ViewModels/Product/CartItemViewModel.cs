namespace Ecommerce.Web.ViewModels;

public class CartItemViewModel {
    public Guid VariationId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string PlatformName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
}