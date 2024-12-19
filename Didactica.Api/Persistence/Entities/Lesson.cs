using System.ComponentModel.DataAnnotations;

namespace Didactica.Persistence.Entities;

public class Lesson: BaseEntity
{
    [MaxLength(100)]
    public required string LessonCode { get; set; }
    [MaxLength(255)]
    public required string LessonName { get; set; }
    public DateTime LessonDate { get; set; }
    [MaxLength(100)]
    public string? Room { get; set; }
    public required LessonType LessonType { get; set; }
}