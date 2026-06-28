using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Web.ViewModels;
public class ProductCardViewModel {
    public Guid ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;

    // Menor número entre as varições
    public decimal LowestPrice { get; set; }
    
    public List<string> Platforms { get; set; } = new();
}