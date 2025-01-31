namespace Didactica.Domain.Dto;

/// <summary>
/// Represents a request to create a new lesson.
/// </summary>
public class CreateLessonRequest
{
    public required string Code { get; set; }
    public required string Name { get; set; }
    public DateTime Date { get; set; }
    public string? Room { get; set; }
    public required int LessonTypeId { get; set; }
}