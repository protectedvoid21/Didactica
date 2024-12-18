namespace Didactica.Persistence.Entities;

public class Lesson
{
    public int Id { get; private set; }
    public required string LessonCode { get; set; }
    public required string LessonName { get; set; }
    public DateTime LessonDate { get; set; }
    public string? Room { get; set; }
    public required LessonType LessonType { get; set; }
}