using ProductWebApi.Models.Enums.Product;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductWebApi.Models;

public class Product
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public ProductCategory Category { get; set; } = ProductCategory.NA;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    public int? LastUpdatedBy { get; set; } = null;

    public int AddedBy { get; set; }
    [ForeignKey("AddedBy")]
    public User? User { get; set; }
}