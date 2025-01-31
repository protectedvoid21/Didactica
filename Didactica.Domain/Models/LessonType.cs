using System.ComponentModel.DataAnnotations;

namespace Didactica.Domain.Models;

/// <summary>
/// Represents the type of a lesson in the system.
/// </summary>
/// <remarks>
/// The LessonType class is used to define and categorize various lesson types.
/// It inherits from the BaseEntity class, providing a unique identifier property.
/// This class includes a required Name property, which describes the type of lesson.
/// </remarks>
public class LessonType : BaseEntity
{
    [MaxLength(100)]
    public required string Name { get; set; }
}