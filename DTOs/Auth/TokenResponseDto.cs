namespace ProductWebApi.DTOs.Auth
{
    public class TokenResponseDto
    {
        public string BearerToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}
