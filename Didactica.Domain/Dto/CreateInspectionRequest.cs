namespace Didactica.Domain.Dto;

/// <summary>
/// Represents the request to create an inspection for a lesson, specifying necessary details.
/// </summary>
/// <remarks>
/// This class is used to provide the required information for creating a new lesson inspection.
/// It includes identifiers for the lesson, inspection method, and teacher, as well as details about
/// whether the inspection is remote, the lesson's environment, and an optional inspection team.
/// </remarks>
public record CreateInspectionRequest
{
    public required int LessonId { get; init; }
    public required int InspectionMethodId { get; init; }
    public required int TeacherId { get; init; }
    public required bool IsRemote { get; init; }
    public required string? LessonEnvironment { get; init; }
    public int InspectionTeamId { get; init; }
}