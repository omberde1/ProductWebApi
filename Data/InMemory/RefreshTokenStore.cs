using ProductWebApi.Models;

namespace ProductWebApi.Data.InMemory
{
    public class RefreshTokenStore
    {
        public int ExpiryInDays = 30;

        public List<RefreshToken> RefreshTokens = [];
    }
}
