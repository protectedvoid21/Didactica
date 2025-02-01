using System.Security.Claims;
using Didactica.Application.Utils;
using Didactica.Domain.Dto;
using Didactica.Domain.Models;
using Didactica.Domain.Models.Persistent;
using Didactica.Domain.Services;
using Didactica.Infrastructure;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Didactica.Application.Services;

public class AccountService : IAccountService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly DidacticaDbContext _dbContext;
    private readonly ITokenService _tokenService;
    
    public AccountService(UserManager<AppUser> userManager, DidacticaDbContext dbContext, ITokenService tokenService)
    {
        _userManager = userManager;
        _dbContext = dbContext;
        _tokenService = tokenService;
    }

    public async Task<Result<AuthTokenResponse>> RegisterAsync(string userName, string email, string password, CancellationToken ct)
    {
        if (await _userManager.FindByNameAsync(userName) is not null)
        {
            return Result.Fail("Username is already taken.");
        }
        var user = new AppUser { UserName = userName, Email = email};
        var createUserResult = await _userManager.CreateAsync(user, password);
        
        if (!createUserResult.Succeeded)
        {
            var errors = createUserResult.Errors.Select(e => e.Description);
            return Result.Fail(errors);
        }

        return Result.Ok();
    }

    public async Task<Result<AuthTokenResponse>> LoginAsync(string userName, string password, CancellationToken ct)
    {
        var user = await _userManager.FindByNameAsync(userName);
        if (user is null || !await _userManager.CheckPasswordAsync(user, password))
        {
            return Result.Fail("Could not log in. Invalid username or password.");
        }

        var isAdmin = await _userManager.IsInRoleAsync(user, RoleNames.Admin);

        var bearerToken = _tokenService.GenerateAuthToken(user.UserName!, isAdmin);
        var refreshToken = _tokenService.GenerateRefreshToken();
        
        _dbContext.Add(new RefreshToken
        {
            Token = refreshToken,
            UserId = user.Id
        });
        await _dbContext.SaveChangesAsync(ct);

        return new AuthTokenResponse(bearerToken, refreshToken);
    }

    public async Task<Result> LogoutAsync(AuthTokenResponse request, CancellationToken ct)
    {
        var principal = _tokenService.GetPrincipalFromExpiredToken(request.Token);
        var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null)
        {
            return Result.Fail("Could not log out. Invalid token.");
        }

        var userGuidId = Guid.Parse(userId);
        var refreshToken = await _dbContext.RefreshTokens
            .Include(x => x.User)
            .FirstOrDefaultAsync(x =>
                x.Token.Equals(request.RefreshToken) && x.UserId.Equals(userGuidId), ct);

        if (refreshToken is null)
        {
            return Result.Fail("Could not log out. Invalid token.");
        }

        _dbContext.Remove(refreshToken);
        await _dbContext.SaveChangesAsync(ct);
        
        return Result.Ok();
    }
}