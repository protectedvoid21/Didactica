using Didactica.Domain.Models;

namespace Didactica.Domain.Dto;

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
    public required GetInspectionTeamResponse GetInspectionTeamResponse { get; set; }
}