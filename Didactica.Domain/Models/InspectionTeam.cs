namespace Didactica.Domain.Models;

public class InspectionTeam : BaseTrackingEntity
{
    public ICollection<Teacher> Teachers { get; set; } = [];
    public ICollection<Inspection> Inspections { get; set; }
}