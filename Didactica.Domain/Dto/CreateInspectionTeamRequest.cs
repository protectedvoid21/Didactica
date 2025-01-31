namespace Didactica.Domain.Dto;

/// <summary>
/// Represents a request to create an inspection team in the system.
/// </summary>
/// <remarks>
/// This request includes a list of teacher IDs who are intended to be part of the inspection team.
/// The provided IDs are validated to ensure the existence of the corresponding teachers in the system.
/// </remarks>
public record CreateInspectionTeamRequest
{
    public List<int>? TeacherIds { get; init; }
}