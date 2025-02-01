using Didactica.Domain.Dto;
using FluentResults;

namespace Didactica.Domain.Services;

public interface IAccountService
{
    Task<Result<AuthTokenResponse>> RegisterAsync(string userName, string email, string password, CancellationToken ct);
    
    Task<Result<AuthTokenResponse>> LoginAsync(string email, string password, CancellationToken ct);

    Task<Result> LogoutAsync(AuthTokenResponse request, CancellationToken ct);
}