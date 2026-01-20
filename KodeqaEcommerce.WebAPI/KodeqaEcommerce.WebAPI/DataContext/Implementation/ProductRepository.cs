using KodeqaEcommerce.WebAPI.DataContext.Interface;
using KodeqaEcommerce.WebAPI.Models;
using System.Text.Json;

namespace KodeqaEcommerce.WebAPI.DataContext.Implementation
{
    public class ProductRepository
        : IProductRepository
    {
        private readonly string _filePath;

        public ProductRepository(IWebHostEnvironment env)
        {
            _filePath = Path.Combine(env.ContentRootPath, "Data", "products.json");
        }

        public Product Get(string productId)
        {
            if (!File.Exists(_filePath))
                throw new FileNotFoundException("products.json is not found");

            var fileContent = File.ReadAllText(_filePath);

            var catalog = JsonSerializer.Deserialize<ProductCatalog>(fileContent, 
                new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (catalog == null || catalog.Products == null)
                throw new InvalidOperationException("Failed to deserialize products.json");

            var product = catalog?.Products.FirstOrDefault(p => p.Id == productId);

            if (product is null)
                throw new KeyNotFoundException($"Product with id: {productId} was not found");

            return product;
        }
    }
}
