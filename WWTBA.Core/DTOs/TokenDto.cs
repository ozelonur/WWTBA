namespace WWTBA.Core.DTOs
{
    public class TokenDto
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime Expiration { get; set; }
        public string Username { get; set; }
    }
}

