using System.Security.Claims;
using Didactica.Domain.Models.Persistent;

namespace Didactica.Application.Common.Models;

public class CurrentUser
{
    public AppUser User { get; set; } = null!;
    public ClaimsPrincipal Principal { get; set; } = default!;

    public Guid Id => User.Id;
    public bool IsAdmin => Principal.IsInRole("admin");
}