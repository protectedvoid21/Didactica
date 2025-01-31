namespace Didactica.Domain.Dto;

/// <summary>
/// Represents the response format containing details of an inspection.
/// </summary>
/// <remarks>
/// This class encapsulates the information related to an inspection such as the teacher,
/// course details, inspection specifics, and associated inspection team data, if any.
/// </remarks>
public class GetInspectionResponse
{
    public required int Id { get; set; }
    public required int TeacherId { get; set; }
    public required string TeacherFirstName { get; set; }
    public required string TeacherLastName { get; set; }
    public required string Course { get; set; }
    public required string CourseType { get; set; }
    public required DateTime Date { get; set; }
    public required bool IsRemote { get; set; }
    public required string? LessonEnvironment { get; set; }
    public required string? Place { get; set; }
    public required GetInspectionTeamResponse? GetInspectionTeamResponse { get; set; }
}