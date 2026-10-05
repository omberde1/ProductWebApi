using ProductWebApi.Models.Enums.Product;

namespace ProductWebApi.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public ProductCategory Category { get; set; } = ProductCategory.NA;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    public int AddedBy { get; set; } = 1;
    public int? LastUpdatedBy { get; set; } = null;
}