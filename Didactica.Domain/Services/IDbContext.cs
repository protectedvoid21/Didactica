using Didactica.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Didactica.Domain.Services;

/// <summary>
/// Represents the contract for the application's database context, providing a set of
/// DbSet properties for accessing entities, as well as methods for saving changes
/// synchronously and asynchronously.
/// </summary>
public interface IDbContext
{
    DbSet<Inspection> Inspections { get; set; }
    DbSet<InspectionForm> InspectionForms { get; set; }
    DbSet<Lesson> Lessons { get; set; }
    DbSet<LessonType> LessonTypes { get; set; }
    DbSet<Teacher> Teachers { get; set; }
    DbSet<InspectionTeam> InspectionTeams { get; set; }
    DbSet<Semester> Semesters { get; set; }
    DbSet<Specialization> Specializations { get; set; }
    DbSet<Degree> Degrees { get; set; }
    DbSet<Appeal> Appeals { get; set; }
    DbSet<AppealStatus> AppealStatuses { get; set; }
    DbSet<AppUser> Users { get; set; }
  
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new());
}