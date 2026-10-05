namespace ProductWebApi.Models.Enums.Product;

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