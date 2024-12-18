namespace Didactica.Persistence.Entities;

public class Specialization
{
    public int Id { get; private set; }
    public required Degree Degree { get; set; }
    public string? SpecializationName { get; set; }
}