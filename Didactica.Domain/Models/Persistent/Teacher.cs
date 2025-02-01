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
    private ICollection<InspectionTeam> _inspectionTeams = [];

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

    public ICollection<InspectionTeam> InspectionTeams
    {
        get => _inspectionTeams;
        set => _inspectionTeams = value;
    }
}