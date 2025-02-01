using Microsoft.AspNetCore.Identity;

namespace Didactica.Domain.Models.Persistent;

public class RefreshToken
{
    public Guid Id { get; set; }
    [ProtectedPersonalData] 
    public required string Token { get; set; }
    public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;

    public Guid UserId { get; set; }
    public AppUser User { get; set; } = default!;
}