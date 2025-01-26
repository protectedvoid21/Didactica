using Didactica.Domain.Models;
using Didactica.Domain.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Didactica.Infrastructure;

public class DidacticaDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>, IDbContext
{
    
    public DbSet<Appeal> Appeals { get; set; }
    public DbSet<AppealStatus> AppealStatuses { get; set; }
    public DbSet<Degree> Degrees { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<InspectionMethod> InspectionMethods { get; set; }
    public DbSet<Inspection> Inspections { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<LessonType> LessonTypes { get; set; }
    public DbSet<Semester> Semesters { get; set; }
    public DbSet<Specialization> Specializations { get; set; }
    public DbSet<InspectionTeam> InspectionTeams { get; set; }
    
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