using System.ComponentModel.DataAnnotations;

namespace Didactica.Api.Persistence.Entities;

public class Teacher: BaseEntity
{
    [MaxLength(255)]
    public required string Name { get; set; }
    
    [MaxLength(255)]
    public required string LastName { get; set; }
    [MaxLength(255)]
    public string? Faculty { get; set; }
    [MaxLength(255)]
    public string? Email { get; set; }
    [MaxLength(20)]
    public string? PhoneNumber { get; set; }
}