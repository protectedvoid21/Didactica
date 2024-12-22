using System.ComponentModel.DataAnnotations;

namespace Didactica.Api.Persistence.Entities;

public class Specialization: BaseEntity
{
    public required Degree Degree { get; set; }
    [MaxLength(255)]
    public string? SpecializationName { get; set; }
}