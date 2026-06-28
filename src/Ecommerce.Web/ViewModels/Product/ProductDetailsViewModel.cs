namespace Ecommerce.Web.ViewModels;

public class ProductDetailsViewModel {
    public Guid ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    
    public List<VariationOptionViewModel> Variations { get; set; } = new();
}
