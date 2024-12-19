using System.ComponentModel.DataAnnotations.Schema;

namespace Didactica.Persistence.Entities;

public class Hospitation: BaseTrackingEntity
{
    public DateTime HospitationDate { get; set; }
    public required HospitationMethod HospitationMethod { get; set; }
}