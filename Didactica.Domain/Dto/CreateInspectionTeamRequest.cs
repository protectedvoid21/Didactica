namespace Didactica.Domain.Dto;

public record CreateInspectionTeamRequest
{
    public List<int> TeacherIds { get; init; }
}