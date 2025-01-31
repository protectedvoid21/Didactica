namespace Didactica.Domain.Dto;

/// <summary>
/// Represents the response containing details of a lesson.
/// </summary>
public class GetLessonResponse
{
    public required string Code { get; set; }
    public required string Name { get; set; }
    public DateTime Date { get; set; }
    public string? Room { get; set; }
    public required int LessonType { get; set; }
}