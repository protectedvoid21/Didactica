using System.ComponentModel.DataAnnotations;

namespace Didactica.Domain.Models.Persistent;

public class Lesson: BaseEntity
{
    [MaxLength(100)]
    public required string Code { get; set; }
    [MaxLength(255)]
    public required string Name { get; set; }
    public DateTime Date { get; set; }

    /// <summary>
    /// Gets or sets the name of the room where the lesson will take place.
    /// </summary>
    /// <remarks>
    /// This property specifies the designated room for a particular lesson.
    /// It is optional and may contain a null value if the room is not specified.
    /// The maximum allowed length for the room name is 100 characters.
    /// </remarks>
    [MaxLength(100)]
    public string? Room { get; set; }
    public required LessonType LessonType { get; set; }
}