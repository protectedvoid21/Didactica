using System.ComponentModel.DataAnnotations;

namespace Didactica.Domain.Models;

public class Specialization : BaseEntity
{
    public required Degree Degree { get; set; }
    [MaxLength(255)]
    public string? Name { get; set; }
}