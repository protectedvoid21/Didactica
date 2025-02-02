using System.ComponentModel.DataAnnotations;

namespace Didactica.Domain.Models.Persistent;

/// <summary>
/// Represents a teacher entity within the system.
/// </summary>
/// <remarks>
/// The <c>Teacher</c> class is a domain entity that stores information about an individual teacher.
/// This includes personal details such as their name, last name, faculty, contact information, and
/// their association with inspection teams.
/// </remarks>
public class Teacher : BaseEntity
{
    [MaxLength(255)]
    public required string Name { get; set; }
    
    [MaxLength(255)]
    public required string LastName { get; set; }
    [MaxLength(255)]
    public string? Faculty { get; set; }
    [MaxLength(255)]
    public string? Email { get; set; }
    [MaxLength(20)]
    public string? PhoneNumber { get; set; }
    
    public Degree? Degree { get; set; }
    public int? DegreeId { get; set; }

    public ICollection<InspectionTeam> InspectionTeams { get; set; } = [];
}