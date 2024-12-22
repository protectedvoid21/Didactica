namespace Didactica.Api.Persistence.Entities;

public class Hospitation : BaseTrackingEntity
{
    public DateTime Date { get; set; }
    public required HospitationMethod HospitationMethod { get; set; }
}