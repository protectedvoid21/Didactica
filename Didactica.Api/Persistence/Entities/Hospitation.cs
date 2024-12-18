namespace Didactica.Persistence.Entities;

public class Hospitation
{
    public int Id { get; private set; }
    public DateTime HospitationDate { get; set; }
    public HospitationMethod HospitationMethod { get; set; }
}