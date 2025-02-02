using System.Security.Claims;

namespace Didactica.Domain.Services;

public interface ITokenService
{
    string GenerateAuthToken(string username, string[] roleNames);
    
    string GenerateRefreshToken();
    
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}