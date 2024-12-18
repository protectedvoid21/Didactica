namespace Didactica.Persistence.Entities;

public class Semester
{
    public int Id { get; private set; }
    public required string Name { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
}