using System.ComponentModel.DataAnnotations;

namespace Didactica.Domain.Models.Persistent;

/// <summary>
/// Represents a specialization associated with a degree within the system.
/// </summary>
/// <remarks>
/// The Specialization class is used to define specific areas of focus or expertise under a given degree.
/// Each specialization includes a reference to its associated degree and an optional name with a maximum
/// length constraint of 255 characters. It inherits from the BaseEntity class, which provides a unique identifier.
/// </remarks>
public class Specialization : BaseEntity
{
    public required Degree Degree { get; set; }
    [MaxLength(255)]
    public string? Name { get; set; }
}