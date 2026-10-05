namespace ProductWebApi.Models;

public class RefreshToken
{
    public int UserId { get; set; }
    public string HashedToken { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
    public bool IsRevoked { get; set; } = false;
}