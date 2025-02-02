namespace Didactica.Domain.Dto;

public class GetInspectionTeamResponse
{
    public required int Id { get; set; }
    public required DateTime CreateDate { get; set; }
    public required InspectionTeamTeacherResponse[] Teachers { get; set; }
}

public class InspectionTeamTeacherResponse
{
    public required int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
}