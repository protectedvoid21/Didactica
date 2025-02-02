using Didactica.Domain.Services;
using Didactica.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Didactica.Application.Services;

public class PrivilegeService : IPrivilegeService
{
    private readonly DidacticaDbContext _dbContext;

    public PrivilegeService(DidacticaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> IsUserInRoleAsync(Guid userId, string role)
    {
        Guid? roleId = (await _dbContext.Roles.FirstOrDefaultAsync(r => r.Name == role))?.Id;
        if (roleId is null)
        {
            return false;
        }
        Guid? adminRoleId = (await _dbContext.Roles.FirstOrDefaultAsync(r => r.Name == "Admin"))?.Id;
        return await _dbContext.UserRoles.AnyAsync(u => u.UserId == userId && (u.RoleId == roleId || u.RoleId == adminRoleId));
    }
}