using System.ComponentModel.DataAnnotations;

namespace Didactica.Domain.Models;

/// <summary>
/// Represents an academic degree within the system.
/// </summary>
/// <remarks>
/// The Degree class inherits from the BaseEntity class and serves as a model for storing information about
/// academic degrees. It includes properties for the name and short name of the degree, both with
/// maximum length constraints.
/// </remarks>
public class Degree : BaseEntity
{
    [MaxLength(100)]
    public required string Name { get; set; }
    [MaxLength(100)]
    public required string Short { get; set; }
}