using System.ComponentModel.DataAnnotations;

namespace Didactica.Api.Persistence.Entities;

public class Lesson: BaseEntity
{
    [MaxLength(100)]
    public required string Code { get; set; }
    [MaxLength(255)]
    public required string Name { get; set; }
    public DateTime Date { get; set; }
    [MaxLength(100)]
    public string? Room { get; set; }
    public required LessonType LessonType { get; set; }
}