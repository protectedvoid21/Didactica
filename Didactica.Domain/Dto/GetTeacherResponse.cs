namespace Didactica.Domain.Dto;

/// <summary>
/// Represents the response model for retrieving a teacher's information.
/// </summary>
public class GetTeacherResponse
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string LastName { get; set; }
    public string? Faculty { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
}