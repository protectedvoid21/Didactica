namespace Didactica.Domain.Dto;

public class CreateLessonRequest
{
    public required string Code { get; set; }
    public required string Name { get; set; }
    public DateTime Date { get; set; }
    public string? Room { get; set; }
    public required int LessonTypeId { get; set; }
}