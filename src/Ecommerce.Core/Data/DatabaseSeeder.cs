using System.Text.Json;
using Ecommerce.Core.Entities;

namespace Ecommerce.Core.Data;

public static class DatabaseSeeder {
    public static void Seed(BankContext context, string jsonPath) {
        // Se já tem categoria, o banco já foi populado, então sai.
        if (context.Set<CategoryEntity>().Any()) return;

        // Lê e converte o arquivo JSON para C#
        var jsonString = File.ReadAllText(jsonPath);
        var seedData = JsonSerializer.Deserialize<SeedDataDto>(jsonString, new JsonSerializerOptions { 
            PropertyNameCaseInsensitive = true 
        });

        if (seedData == null) return;

        // 1. Dicionários para guardar os IDs reais gerados no banco
        var categoryDict = new Dictionary<string, Guid>();
        foreach (var catName in seedData.Categories) {
            var category = new CategoryEntity(catName);
            context.Set<CategoryEntity>().Add(category);
            categoryDict[catName] = category.Id; // Ex: ["Metroidvania"] = 0000-1111...
        }

        var platformDict = new Dictionary<string, Guid>();
        foreach (var platName in seedData.Platforms) {
            var platform = new PlatformEntity(platName);
            context.Set<PlatformEntity>().Add(platform);
            platformDict[platName] = platform.Id;
        }

        // Salva para efetivar as chaves no banco
        context.SaveChanges();

        // 2. Criação dos Produtos e Variações
        foreach (var prodDto in seedData.Products) {
            // Busca o Guid da categoria pelo nome que estava no JSON
            var categoryId = categoryDict[prodDto.CategoryName];
            
            var product = new ProductEntity(prodDto.Name, prodDto.Description, prodDto.ImageUrl, categoryId);
            context.Set<ProductEntity>().Add(product);

            foreach (var varDto in prodDto.Variations) {
                // Busca o Guid da plataforma pelo nome
                var platformId = platformDict[varDto.PlatformName];
                
                var variation = new ProductVariationEntity(varDto.Price, varDto.Stock, product.Id, platformId);
                context.Set<ProductVariationEntity>().Add(variation);
            }
        }

        context.SaveChanges();
    }
}

// --- CLASSES AUXILIARES PARA LER O JSON ---
public class SeedDataDto {
    public List<string> Categories { get; set; } = new();
    public List<string> Platforms { get; set; } = new();
    public List<ProductSeedDto> Products { get; set; } = new();
}

public class ProductSeedDto {
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public List<VariationSeedDto> Variations { get; set; } = new();
}

public class VariationSeedDto {
    public string PlatformName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }
}