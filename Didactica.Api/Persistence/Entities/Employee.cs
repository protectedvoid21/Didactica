namespace Didactica.Persistence.Entities;

public class Employee
{
    public int Id { get; private set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public string? Faculty { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
}