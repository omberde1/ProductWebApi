namespace ProductWebApi.Data.InMemory;

using ProductWebApi.Models;

public class ProductStore
{
    private static int LastTrackedId = 0;
    public List<Product> Products { get; set; } = 
    [
        new Product
        {
            Id = 1,
            Name = "Wireless Mouse",
            Price = 799,
            Stock = 25,
            Category = ProductCategory.Electronics,
            CreatedAt = new DateTime(2026, 1, 10)
        },

        new Product
        {
            Id = 2,
            Name = "Mechanical Keyboard",
            Price = 2499,
            Stock = 15,
            Category = ProductCategory.Electronics,
            CreatedAt = new DateTime(2026, 1, 15)
        },

        new Product
        {
            Id = 3,
            Name = "USB-C Cable",
            Price = 399,
            Stock = 50,
            Category = ProductCategory.Accessories,
            CreatedAt = new DateTime(2026, 2, 5)
        },

        new Product
        {
            Id = 4,
            Name = "Laptop Stand",
            Price = 1299,
            Stock = 12,
            Category = ProductCategory.Accessories,
            CreatedAt = new DateTime(2026, 2, 18)
        },

        new Product
        {
            Id = 5,
            Name = "Office Chair",
            Price = 8999,
            Stock = 8,
            Category = ProductCategory.Furniture,
            CreatedAt = new DateTime(2026, 3, 2)
        },

        new Product
        {
            Id = 6,
            Name = "Study Table",
            Price = 6499,
            Stock = 6,
            Category = ProductCategory.Furniture,
            CreatedAt = new DateTime(2026, 3, 12)
        },

        new Product
        {
            Id = 7,
            Name = "Bluetooth Speaker",
            Price = 1799,
            Stock = 20,
            Category = ProductCategory.Electronics,
            CreatedAt = new DateTime(2026, 4, 1)
        },

        new Product
        {
            Id = 8,
            Name = "Jeans",
            Price = 2199,
            Stock = 10,
            Category = ProductCategory.Clothing,
            CreatedAt = new DateTime(2026, 4, 15)
        },

        new Product
        {
            Id = 9,
            Name = "Notebook",
            Price = 199,
            Stock = 100,
            Category = ProductCategory.Stationery,
            CreatedAt = new DateTime(2026, 5, 3)
        },

        new Product
        {
            Id = 10,
            Name = "Ball Pen Pack",
            Price = 149,
            Stock = 75,
            Category = ProductCategory.Stationery,
            CreatedAt = new DateTime(2026, 5, 10)
        },

        new Product
        {
            Id = 11,
            Name = "Water Bottle",
            Price = 599,
            Stock = 30,
            Category = ProductCategory.Home,
            CreatedAt = new DateTime(2026, 6, 1)
        },

        new Product
        {
            Id = 12,
            Name = "Backpack",
            Price = 1599,
            Stock = 18,
            Category = ProductCategory.Bags,
            CreatedAt = new DateTime(2026, 6, 20)
        }
    ];

    public int GetIncrementedId()
    {
        // DB Identity Id Mock
        if (LastTrackedId == 0)
        { 
            var lastInsertedProduct = Products.OrderByDescending(p => p.Id).FirstOrDefault();
            LastTrackedId = lastInsertedProduct?.Id ?? 0;
        }
        return ++LastTrackedId;
    }
}