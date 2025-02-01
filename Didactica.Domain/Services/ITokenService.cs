using System.Security.Claims;

namespace Didactica.Domain.Services;

public interface ITokenService
{
    string GenerateAuthToken(string username, bool isAdmin = false);
    string GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}