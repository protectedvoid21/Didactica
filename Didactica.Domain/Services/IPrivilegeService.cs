namespace Didactica.Domain.Services;

public interface IPrivilegeService
{
    Task<bool> IsUserInRoleAsync(Guid userId, string role);
}