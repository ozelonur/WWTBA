namespace WWTBA.Shared.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(Dictionary<string, string> claims);
        string GenerateRefreshToken();
    }
}