using Didactica.Persistence.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Didactica.Persistence;

public class DidacticaDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
{
    public DidacticaDbContext(DbContextOptions<DidacticaDbContext> options) : base(options)
    {
    }
    
    private void UpdateBaseTrackingEntities()
    {
        var entries = ChangeTracker.Entries<BaseTrackingEntity>().ToList();
        var now = DateTime.UtcNow;
        foreach (var entry in entries)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedOn = now;
                    entry.Entity.UpdatedOn = now;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedOn = now;
                    break;
            }
        }
    }
    
    public override int SaveChanges()
    {
        UpdateBaseTrackingEntities();
        
        return base.SaveChanges();
    }
    
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        UpdateBaseTrackingEntities();

        return base.SaveChangesAsync(cancellationToken);
    }
}