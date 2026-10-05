using ProductWebApi.Models;

namespace ProductWebApi.Data.InMemory
{
    public class UserStore
    {
        // #IMP# C# List now uses "[]" simplified, no "new ()" needed
        public List<User> Users =
        [
            new User { Id = 1, Username = "omberde1", Password = "123456", Role = "admin" },
            new User { Id = 2, Username = "john_doe", Password = "654321", Role = "customer" },
            new User { Id = 3, Username = "mike_123", Password = "123123", Role = "product_admin" }
        ];
    }
}
