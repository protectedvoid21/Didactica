using System.ComponentModel.DataAnnotations;

namespace Didactica.Domain.Models;

public class InspectionMethod : BaseEntity
{
    [MaxLength(255)]
    public required string Name { get; set; }
}