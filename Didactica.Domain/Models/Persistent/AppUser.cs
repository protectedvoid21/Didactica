using Microsoft.AspNetCore.Identity;

namespace Didactica.Domain.Models.Persistent;

public class AppUser : IdentityUser<Guid>
{
    public Teacher? Teacher { get; set; }
    public int? TeacherId { get; set; }
}