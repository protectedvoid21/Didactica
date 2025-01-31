namespace Didactica.Domain.Dto;

/// <summary>
/// Represents the response containing information about an inspection team.
/// </summary>
/// <remarks>
/// This class provides details about the inspection team such as the team's ID
/// and a collection of teachers associated with the team. Each teacher is defined
/// as a tuple containing their ID and name.
/// </remarks>
public class GetInspectionTeamResponse
{
    public required int Id { get; set; }
    public Tuple<int, string>[] Teachers { get; set; }
}