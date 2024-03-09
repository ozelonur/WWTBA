namespace WWTBA.Core.Models
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public int UserId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Expires { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime? Revoked { get; set; }
        public User User { get; set; }
    }
}

