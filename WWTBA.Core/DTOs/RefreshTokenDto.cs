using System;

namespace WWTBA.Core.DTOs
{
    public class RefreshTokenDto
    {
        public string Token { get; set; }
        public int UserId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Expires { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime? Revoked { get; set; }
    }
}