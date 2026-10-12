using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductWebApi.Models;

public class RefreshToken
{
    [Key]
    public int Id { get; set; }
    public string HashedToken { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
    public bool IsRevoked { get; set; } = false;

    public int UserId { get; set; }
    [ForeignKey("UserId")]
    public User? User { get; set; }
}