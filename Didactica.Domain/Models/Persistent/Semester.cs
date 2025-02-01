using System.ComponentModel.DataAnnotations;

namespace Didactica.Domain.Models.Persistent;

/// <summary>
/// Represents an academic semester within the system.
/// </summary>
/// <remarks>
/// The Semester class is a part of the academic structure used to define
/// specific periods within an academic year. Each semester has a name,
/// a start date, and an end date. It inherits from the BaseEntity class,
/// which provides a unique identifier.
/// </remarks>
public class Semester : BaseEntity
{
    [MaxLength(100)] 
    public required string Name { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
}