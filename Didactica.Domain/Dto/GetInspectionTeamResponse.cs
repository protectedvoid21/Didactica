using Didactica.Domain.Models;

namespace Didactica.Domain.Dto;

public class GetInspectionTeamResponse
{
    public required int Id { get; set; }
    public Tuple<int, string>[] Teachers { get; set; }
}