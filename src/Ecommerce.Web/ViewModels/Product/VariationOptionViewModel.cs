namespace Ecommerce.Web.ViewModels;

public class VariationOptionViewModel {
    public Guid VariationId { get; set; }
    public string PlatformName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }
}