namespace Didactica.Domain.Dto;

public record CreateInspectionRequest
{
    public required int LessonId { get; init; }
    public required int InspectionMethodId { get; init; }
    public required int TeacherId { get; init; }
    public required bool IsRemote { get; init; }
    public required string? LessonEnvironment { get; init; }
}