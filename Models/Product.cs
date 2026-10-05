namespace ProductWebApi.Models;

// #IMP# Enums are idx based and not string, so 1,,2,etc gets passed not the Enum Name val; Could also manually apply idx
public enum ProductCategory
{
    NA = 0,
    Electronics = 1,
    Accessories = 2,
    Furniture = 3,
    Clothing = 4,
    Stationery = 5,
    Home = 6,
    Bags = 7
}

public enum ProductSortByFilter
{ 
    PriceAsc = 0,
    PriceDesc = 1,
    NameAsc = 2,
    NameDesc = 3,
    StockAsc = 4,
    StockDesc = 5,
    CreatedAtAsc = 6,
    CreatedAtDesc = 7
}

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