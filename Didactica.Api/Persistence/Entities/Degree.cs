namespace Didactica.Persistence.Entities;

public class Degree
{
    public int Id { get; private set; }
    public required string Name { get; set; }
    public required string Short { get; set; }
}